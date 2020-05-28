using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BuildingManager : MonoBehaviour
{
    private static BuildingManager _instance;
    public GameObject ReloadAnimation;

    [FormerlySerializedAs("ConstructedTowers")]
    private Dictionary<Vector2, Building> ConstructedBuildings;

    public static Vector2 LastPositionTouched;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this);
        }

        ConstructedBuildings = new Dictionary<Vector2, Building>();
    }

    public static GameObject GetReloadAnimation()
    {
        return _instance.ReloadAnimation;
    }

    public static Dictionary<Vector2, Building> GetBuildings()
    {
        return _instance.ConstructedBuildings;
    }

    public static void AddBuilding(Vector2 pos, Building building)
    {
        _instance.ConstructedBuildings.Add(pos, building);
        LastPositionTouched = pos;
        if (building.Model != null)
        {
            foreach (Effect effect in building.Model.AdjacencyEffects)
            {
                effect.Activate();
            }
        }

        /*
         * This is really ugly, but I think it's needed. When we place a new tower, for example, we need to check
         * its neighbours to see if there's an adjacency effect we need to apply to the newly constructed building.
         * This might just be a performance killer for mobile though -- there's gotta be a better way to do this.
         */
        List<Vector2> positionsToCheck = new List<Vector2>();
        positionsToCheck.Add(LastPositionTouched                + Vector2.up);    // N
        positionsToCheck.Add(LastPositionTouched + Vector2.up   + Vector2.left);  // NW
        positionsToCheck.Add(LastPositionTouched + Vector2.up   + Vector2.right); // NE
        positionsToCheck.Add(LastPositionTouched                + Vector2.down);  // S
        positionsToCheck.Add(LastPositionTouched + Vector2.down + Vector2.left);  // SW
        positionsToCheck.Add(LastPositionTouched + Vector2.down + Vector2.right); // SE
        positionsToCheck.Add(LastPositionTouched                + Vector2.right); // E
        positionsToCheck.Add(LastPositionTouched                + Vector2.left);  // W

        foreach (Vector2 position in positionsToCheck)
        {
            Building b = GetBuildingAt(position);
            if (b != null && b.Model != null)
            {
                LastPositionTouched = position;
                foreach (Effect adjacencyEffect in b.Model.AdjacencyEffects)
                {
                    adjacencyEffect.Activate();
                }
            }
        }
    }

    public static void RemoveBuilding(Vector2 pos)
    {
        Building building = _instance.ConstructedBuildings[pos];
        LastPositionTouched = pos;
        if (building.Model != null)
        {
            foreach (Effect effect in building.Model.AdjacencyEffects)
            {
                effect.Deactivate();
            }
        }

        /*
         * This is really ugly, but I think it's needed. When we place a new tower, for example, we need to check
         * its neighbours to see if there's an adjacency effect we need to apply to the newly constructed building.
         * This might just be a performance killer for mobile though -- there's gotta be a better way to do this.
         */
        List<Vector2> positionsToCheck = new List<Vector2>();
        positionsToCheck.Add(LastPositionTouched                + Vector2.up);    // N
        positionsToCheck.Add(LastPositionTouched + Vector2.up   + Vector2.left);  // NW
        positionsToCheck.Add(LastPositionTouched + Vector2.up   + Vector2.right); // NE
        positionsToCheck.Add(LastPositionTouched                + Vector2.down);  // S
        positionsToCheck.Add(LastPositionTouched + Vector2.down + Vector2.left);  // SW
        positionsToCheck.Add(LastPositionTouched + Vector2.down + Vector2.right); // SE
        positionsToCheck.Add(LastPositionTouched                + Vector2.right); // E
        positionsToCheck.Add(LastPositionTouched                + Vector2.left);  // W

        foreach (Vector2 position in positionsToCheck)
        {
            Building b = GetBuildingAt(position);
            LastPositionTouched = position;
            if (b != null && b.Model != null)
            {
                foreach (Effect adjacencyEffect in b.Model.AdjacencyEffects)
                {
                    adjacencyEffect.Activate();
                }
            }
        }


        _instance.ConstructedBuildings.Remove(pos);
    }

    public static void StartTurn()
    {
        foreach (KeyValuePair<Vector2, Building> tower in _instance.ConstructedBuildings)
        {
            tower.Value.DoStartOfTurnEffects();
        }
    }

    public static Building GetBuildingAt(Vector2 pos)
    {
        return _instance.ConstructedBuildings.ContainsKey(pos) ? _instance.ConstructedBuildings[pos] : null;
    }
}