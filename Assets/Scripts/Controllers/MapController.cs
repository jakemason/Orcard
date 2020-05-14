using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public static MapController Instance;
    public List<Transform> Waypoints;
    public GameObject Road;
    public Dictionary<Vector2, bool> IsSpaceOccupied;

    public Sprite PathSprite;
    public int PathDeviations = 2;
    public int XBound = 6;
    public int YBound = 3;
    private const int PATH_DEFAULT_Y_POS = 1;
    private readonly Vector2 _startPos = new Vector2(13, PATH_DEFAULT_Y_POS);
    private readonly Vector2 _endPos = new Vector2(-23,  PATH_DEFAULT_Y_POS);


    public List<Vector2> PathingAnchors;
    public GameObject PathingAnchorPrefab;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }


        Waypoints       = new List<Transform>();
        PathingAnchors  = new List<Vector2>();
        IsSpaceOccupied = new Dictionary<Vector2, bool>();


        PathingAnchors.Add(_endPos);
        Instantiate(PathingAnchorPrefab, _endPos, Quaternion.identity, gameObject.transform);

        GenerateRoads();

        PathingAnchors.Add(_startPos);
        Instantiate(PathingAnchorPrefab, _startPos, Quaternion.identity, gameObject.transform);

        Transform[] children = GetComponentsInChildren<Transform>();
        foreach (Transform child in children)
        {
            if (child.gameObject == gameObject) continue;
            Waypoints.Add(child);
        }

        SetupOccupiedSpaces();
    }

    private void GenerateRoads()
    {
        List<int> branchPoints = new List<int>();
        //we use the minimum gap list to be sure we can't have an X distance of less than 2 between points
        List<int> minimumGapPoints = new List<int>();
        while (branchPoints.Count < 4)
        {
            int rand = Random.Range(-XBound, XBound + 1);
            if (!minimumGapPoints.Contains(rand))
            {
                minimumGapPoints.Add(rand - 1); //prevent width of 1
                minimumGapPoints.Add(rand + 1); //prevent width of 1
                minimumGapPoints.Add(rand);
                branchPoints.Add(rand);
            }
        }

        branchPoints.Sort(); // sort our selected points to make sure we build between adjacent points

        //we need to do this loop twice, once for each "pair of points"
        for (int i = 0; i <= 2; i += 2)
        {
            int yDistance = Random.Range(-YBound, YBound + 1); //determine how "deep" the path offshoot will go
            while (yDistance == PATH_DEFAULT_Y_POS)            //make sure we deviate from the natural path at 0
            {
                yDistance = Random.Range(-YBound, YBound + 1);
            }

            //get our first set of points and determine how we close the gap between the two
            int dx = branchPoints[i] < branchPoints[i + 1] ? 1 : -1;
            for (int x = branchPoints[i]; x != branchPoints[i + 1]; x += dx)
            {
                Vector2 position = new Vector2(x, yDistance);
                Instantiate(Road, position, Quaternion.identity);
            }

            //for the vertical change between Y values
            int dy = PATH_DEFAULT_Y_POS < yDistance ? 1 : -1;
            for (int y = PATH_DEFAULT_Y_POS; y != yDistance + dy; y += dy)
            {
                Vector2 position = new Vector2(branchPoints[i], y);
                Instantiate(Road, position, Quaternion.identity);

                position = new Vector2(branchPoints[i + 1], y);
                Instantiate(Road, position, Quaternion.identity);
            }

            PathingAnchors.Add(new Vector2(branchPoints[i],     yDistance));
            PathingAnchors.Add(new Vector2(branchPoints[i + 1], yDistance));

            Instantiate(PathingAnchorPrefab, new Vector2(branchPoints[i], PATH_DEFAULT_Y_POS), Quaternion.identity,
                gameObject.transform);
            Instantiate(PathingAnchorPrefab, new Vector2(branchPoints[i], yDistance), Quaternion.identity,
                gameObject.transform);


            Instantiate(PathingAnchorPrefab, new Vector2(branchPoints[i + 1], yDistance), Quaternion.identity,
                gameObject.transform);
            Instantiate(PathingAnchorPrefab, new Vector2(branchPoints[i + 1], PATH_DEFAULT_Y_POS), Quaternion.identity,
                gameObject.transform);
        }

        //rest of the straight path -- going "right" to left for now...
        for (int x = (int) _endPos.x; x < (int) _startPos.x; x++)
        {
            if (x < branchPoints[0] || x > branchPoints[3])
            {
                Instantiate(Road, new Vector2(x, PATH_DEFAULT_Y_POS), Quaternion.identity);
            }

            if (x > branchPoints[1] && x < branchPoints[2])
            {
                Instantiate(Road, new Vector2(x, PATH_DEFAULT_Y_POS), Quaternion.identity);
            }
        }
    }

#if UNITY_EDITOR
    private void Update()
    {
        DrawDebugLines();
    }
#endif
    public static bool IsOccupied(Vector2 pos)
    {
        return Instance.IsSpaceOccupied.ContainsKey(pos);
    }

    private void SetupOccupiedSpaces()
    {
        for (int i = 0; i < Waypoints.Count - 1; i++)
        {
            Transform current = Waypoints[i];
            Transform next    = Waypoints[i + 1];
            float     dist    = Vector3.Distance(current.position, next.position);
            Vector3   unit    = next.position - current.position;
            unit.Normalize();
            for (int j = 0; j < dist; j++)
            {
                IsSpaceOccupied.Add(current.position + (unit * j), true);
                //Note: Uncomment to spawn in GameObjects per "tile" occupied. Occasionally useful for debugging.
                //Instantiate(Road, current.position + (unit * j), Quaternion.identity, this.gameObject.transform);
            }
        }
    }

    private void OnValidate()
    {
        DrawDebugLines();
    }

    private void DrawDebugLines()
    {
        for (int i = 0; i < Waypoints.Count - 1; i++)
        {
            Vector3 start = Waypoints[i].transform.position;
            Vector3 end   = Waypoints[i + 1].transform.position;
            Debug.DrawLine(start, end, Color.green);
        }
    }

    public static IEnumerable<Transform> GetWaypoints()
    {
        return Instance.Waypoints;
    }
}