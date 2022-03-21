using System.Collections.Generic;
using UnityEngine;

public class BuildingsGrid : MonoBehaviour
{
    private Vector2Int gridSize;
    private Building[,] grid;
    private Building returnBuilding;
    [SerializeField]
    private Building wallBuilding;

    private void Awake()
    {
        gridSize = new Vector2Int(((int)transform.localScale.x), ((int)transform.localScale.z));
        grid = new Building [gridSize.x, gridSize.y];
    }
    private void Start()
    {
        WallBuilding();
    }

    private void WallBuilding()
    {
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                if (x == 0 || y == 0 || x == gridSize.x - 1 || y == gridSize.y - 1)
                {
                    Vector2Int placeToBuild = new Vector2Int(x, y);
                    if (IsPlaceAvailable(placeToBuild))
                    {
                        grid[placeToBuild.x, placeToBuild.y] = Instantiate(
                            wallBuilding, new Vector3(x, transform.position.y + transform.localScale.y/2, y), Quaternion.identity); 
                    }
                }
            }
        }
    }

    public Building Demolition(Vector2Int placeToDestroy)
    {
        if (IsPlaceOccupiedByBuilding(placeToDestroy))
        {
            returnBuilding = grid[placeToDestroy.x, placeToDestroy.y];
            grid[placeToDestroy.x, placeToDestroy.y] = null;
            return returnBuilding;
        }
        return null;
    }

    public bool Installation(Building newBuilding, Vector2Int placeToBuild)
    {
        if (IsPlaceAvailable(placeToBuild))
        {
            grid[placeToBuild.x, placeToBuild.y] = Instantiate(
            newBuilding, new Vector3(placeToBuild.x, transform.position.y + transform.localScale.y/2, placeToBuild.y), Quaternion.identity);
            return true;
        }
        return false;
    }

    private bool IsPlaceAvailable(Vector2Int placeToCheak)
    {
        if (placeToCheak.x >= 0 && placeToCheak.x < gridSize.x
            && placeToCheak.y >= 0 && placeToCheak.y < gridSize.y)
        {
            if (grid[placeToCheak.x, placeToCheak.y] == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    private bool IsPlaceOccupiedByBuilding(Vector2Int placeToCheak)
    {
        if (placeToCheak.x >= 0 && placeToCheak.x < gridSize.x
            && placeToCheak.y >= 0 && placeToCheak.y < gridSize.y)
        {
            if (grid[placeToCheak.x, placeToCheak.y])
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    public bool CheakPlaceToMove(Vector2Int placeToCheak)
    {
        if (IsPlaceOccupiedByBuilding(placeToCheak))
        {
            if (grid[placeToCheak.x, placeToCheak.y].IsPassable)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }
}
