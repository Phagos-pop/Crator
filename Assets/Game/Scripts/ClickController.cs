using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickController : MonoBehaviour
{
    private Building building;
    private Plane groundPlane;
    public float valueOfLifting = 1;

    private void Start()
    {
        groundPlane = new Plane(Vector3.up, Vector3.zero + new Vector3(0f, valueOfLifting, 0f));
    }

    private void Update()
    {
        if (building)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (groundPlane.Raycast(ray, out float position))
            {
                Vector3 worldPosition = ray.GetPoint(position);

                int x = Mathf.RoundToInt(worldPosition.x);
                int y = Mathf.RoundToInt(worldPosition.z);

                building.transform.position = new Vector3(x, valueOfLifting, y);
            }
        }
    }
    public void ClickOnPlace()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject.GetComponent<PlaceToBuild>())
            {
                PlaceToBuild placeToBuild = hit.collider.gameObject.GetComponent<PlaceToBuild>();
                Vector2Int rayVector2 = Vector2Int.zero;

                if (groundPlane.Raycast(ray, out float position))
                {
                    Vector3 worldPosition = ray.GetPoint(position);
                    rayVector2 = new Vector2Int(Mathf.RoundToInt(worldPosition.x), Mathf.RoundToInt(worldPosition.z));
                }

                if (building)
                {
                    if (placeToBuild.InstallationBuilding(building, rayVector2))
                    {
                        Destroy(building.gameObject);
                    }  
                }
                else
                {                   
                    building = placeToBuild.DemolitionOfBuilding(rayVector2);
                }
            }
        }
    }

    public void StartPlacingBuilding(Building buildingPrefab)
    {
        if (building)
        {
            Destroy(building.gameObject);
        }

        building = Instantiate(buildingPrefab);
    }

    public void ClearBuilding()
    {
        Destroy(building.gameObject);
    }
}
