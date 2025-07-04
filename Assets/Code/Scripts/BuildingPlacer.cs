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
            int cost = UpgradeManager.GetReducedCost(baseCost); // <-- Utilisation de la réduction de coût
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
}
