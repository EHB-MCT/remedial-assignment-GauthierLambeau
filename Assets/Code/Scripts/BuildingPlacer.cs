using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingPlacer : MonoBehaviour
{
    public GameObject[] buildingPrefabs;
    public int[] buildingCosts;
    private int selectedBuildingIndex = 0;

    public LayerMask placementLayer;

    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        if (Input.GetMouseButtonDown(0))
        {
            PlaceBuilding();
        }
    }

    public void SelectBuilding(int index)
    {
        if (index >= 0 && index < buildingPrefabs.Length)
        {
            selectedBuildingIndex = index;
        }
    }

    void PlaceBuilding()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f, placementLayer))
        {
            int baseCost = buildingCosts[selectedBuildingIndex];
            int cost = UpgradeManager.GetReducedCost(baseCost); 
            if (MoneyManager.Instance.SpendMoney(cost))
            {
                Instantiate(buildingPrefabs[selectedBuildingIndex], hit.point, Quaternion.identity);
            }
            else
            {
                Debug.Log("Pas assez d'argent pour placer ce bâtiment.");
            }
        }
    }


    public void ForceSpawnBuilding(string buildingType)
{
    for (int i = 0; i < buildingPrefabs.Length; i++)
    {
        var prefab = buildingPrefabs[i];
        var building = prefab.GetComponent<Building>();
        if (building != null && building.buildingType == buildingType)
        {
     
            Vector3 spawnPos = new Vector3(Random.Range(-5,5), 0, Random.Range(-5,5));
            Instantiate(buildingPrefabs[i], spawnPos, Quaternion.identity);
            break;
        }
    }
}

}
