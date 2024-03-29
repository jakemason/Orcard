﻿using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    // @formatter:off
    public static MapController Instance;
    public List<Transform> Waypoints;
    public GameObject Road;
    public GameObject BuildingBlocker;
    public List<Sprite> ObstacleSprites;
    public Dictionary<Vector2, bool> IsSpaceOccupied;

    [Header("Path Sprites")]
    public Sprite Horizontal;
    public Sprite Vertical;
    public Sprite TopLeft;
    public Sprite BottomLeft;
    public Sprite TopRight;
    public Sprite BottomRight;

    public int ObstaclesToSpawn;
    public int PathDeviations = 2;
    public int XBound = 6;
    public int YBound = 2;
    private const int PATH_DEFAULT_Y_POS = 1;
    private readonly Vector2 _startPos = new Vector2(13, PATH_DEFAULT_Y_POS);
    private readonly Vector2 _endPos = new Vector2(-10,  PATH_DEFAULT_Y_POS);

    //these are the anchors the orcs will move along asa they go from Transform to Transform
    public List<Vector2> PathingAnchors;
    public GameObject PathingAnchorPrefab;
    // @formatter:on

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
        GenerateObstacles();
    }

    private void GenerateObstacles()
    {
        int obstaclesCreated = 0;
        int crashControl     = 0;
        while (obstaclesCreated < ObstaclesToSpawn && crashControl < 500)
        {
            crashControl++;
            int     x   = Random.Range(-XBound, XBound + 1);
            int     y   = Random.Range(-YBound, YBound + 2);
            Vector2 pos = new Vector2(x, y);
            if (IsOccupied(pos)) continue;

            int             spriteIndex = Random.Range(0, ObstacleSprites.Count);
            GameObject      obstacle    = Instantiate(BuildingBlocker, pos, Quaternion.identity);
            BuildingBlocker blocker     = obstacle.GetComponent<BuildingBlocker>();
            blocker.Sprite           = ObstacleSprites[spriteIndex];
            blocker.IsIndestructible = false;
            IsSpaceOccupied[pos]     = true;
            obstaclesCreated++;
        }
    }

    private void GenerateRoads()
    {
        int       branchPointsToCreate = PathDeviations * 2;
        List<int> branchPoints         = new List<int>();
        //we use the minimum gap list to be sure we can't have an X distance of less than 3 between points
        List<int> minimumGapPoints = new List<int>();
        while (branchPoints.Count < branchPointsToCreate)
        {
            int rand = Random.Range(-XBound, XBound + 1);
            if (minimumGapPoints.Contains(rand)) continue;

            minimumGapPoints.Add(rand - 1); //make sure the minimum "width" of any path is at least 3
            minimumGapPoints.Add(rand + 1); //make sure the minimum "width" of any path is at least 3
            minimumGapPoints.Add(rand);
            branchPoints.Add(rand);
        }

        branchPoints.Sort(); // sort our selected points to make sure we build between adjacent points

        //we need to do this loop twice, once for each "pair of points"
        for (int i = 0; i <= branchPointsToCreate / 2; i += 2)
        {
            int yDistance = Random.Range(-YBound, YBound + 2); //determine how "deep" the path offshoot will go

            while (yDistance == PATH_DEFAULT_Y_POS) //make sure we deviate from the natural path at 0
            {
                yDistance = Random.Range(-YBound, YBound + 2);
            }

            //get our first set of points and determine how we close the gap between the two
            int dx = branchPoints[i] < branchPoints[i + 1] ? 1 : -1;
            for (int x = branchPoints[i] + dx; x != branchPoints[i + 1]; x += dx)
            {
                Vector2 position = new Vector2(x, yDistance);
                Instantiate(BuildingBlocker, position, Quaternion.identity);
                GameObject go =
                    Instantiate(Road, position, Quaternion.identity);
                SpriteRenderer rend = go.GetComponent<SpriteRenderer>();
                rend.sprite = Horizontal;
            }

            //for the vertical change between Y values
            int dy = PATH_DEFAULT_Y_POS < yDistance ? 1 : -1;
            for (int y = PATH_DEFAULT_Y_POS; y != yDistance + dy; y += dy)
            {
                Vector2 position = new Vector2(branchPoints[i], y);
                Instantiate(BuildingBlocker, position, Quaternion.identity);
                GameObject     go   = Instantiate(Road, position, Quaternion.identity);
                SpriteRenderer rend = go.GetComponent<SpriteRenderer>();
                //determine sprite type, either it turns or it's a vertical
                if (y == PATH_DEFAULT_Y_POS)
                {
                    rend.sprite = dy == 1 ? BottomRight : TopRight;
                }
                else if (y == yDistance)
                {
                    rend.sprite = dy == 1 ? TopLeft : BottomLeft;
                }
                else
                {
                    rend.sprite = Vertical;
                }

                position = new Vector2(branchPoints[i + 1], y);
                Instantiate(BuildingBlocker, position, Quaternion.identity);
                go   = Instantiate(Road, position, Quaternion.identity);
                rend = go.GetComponent<SpriteRenderer>();
                //determine sprite type, either it turns or it's a vertical
                if (y == PATH_DEFAULT_Y_POS)
                {
                    rend.sprite = dy == 1 ? BottomLeft : TopLeft;
                }
                else if (y == yDistance)
                {
                    rend.sprite = dy == 1 ? TopRight : BottomRight;
                }
                else
                {
                    rend.sprite = Vertical;
                }
            }

            PathingAnchors.Add(new Vector2(branchPoints[i],     yDistance));
            PathingAnchors.Add(new Vector2(branchPoints[i + 1], yDistance));

            /*
             * Create the rest of the PathingAnchor transforms
             */
            Instantiate(PathingAnchorPrefab,
                new Vector2(branchPoints[i], PATH_DEFAULT_Y_POS),
                Quaternion.identity,
                gameObject.transform);


            Instantiate(PathingAnchorPrefab,
                new Vector2(branchPoints[i], yDistance),
                Quaternion.identity,
                gameObject.transform);


            Instantiate(PathingAnchorPrefab,
                new Vector2(branchPoints[i + 1], yDistance),
                Quaternion.identity,
                gameObject.transform);


            Instantiate(PathingAnchorPrefab,
                new Vector2(branchPoints[i + 1], PATH_DEFAULT_Y_POS),
                Quaternion.identity,
                gameObject.transform);
        }

        //rest of the straight path -- going "right" to left for now...
        for (int x = (int) _endPos.x; x < (int) _startPos.x; x++)
        {
            if (x < branchPoints[0] || x > branchPoints[3])
            {
                Instantiate(BuildingBlocker, new Vector2(x, PATH_DEFAULT_Y_POS), Quaternion.identity);
                GameObject go =
                    Instantiate(Road, new Vector2(x, PATH_DEFAULT_Y_POS), Quaternion.identity);
                SpriteRenderer rend = go.GetComponent<SpriteRenderer>();
                rend.sprite = Horizontal;
            }

            if (x > branchPoints[1] && x < branchPoints[2])
            {
                Instantiate(BuildingBlocker, new Vector2(x, PATH_DEFAULT_Y_POS), Quaternion.identity);
                GameObject go =
                    Instantiate(Road, new Vector2(x, PATH_DEFAULT_Y_POS), Quaternion.identity);
                SpriteRenderer rend = go.GetComponent<SpriteRenderer>();
                rend.sprite = Horizontal;
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