using UnityEngine;

public class PlaceToBuild : MonoBehaviour
{
    public BuildingsGrid buildingsGrid;

    public Building DemolitionOfBuilding(Vector2Int rayVector2)
    {
        return buildingsGrid.Demolition(rayVector2);
    }

    public bool InstallationBuilding(Building newBuilding , Vector2Int rayVector2)
    {
        return buildingsGrid.Installation(newBuilding, rayVector2);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0.88f, 0f, 1f, 0.3f);
        Gizmos.DrawCube(transform.position, transform.localScale);
    }
}
