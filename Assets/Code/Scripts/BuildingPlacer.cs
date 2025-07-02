using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingPlacer : MonoBehaviour
{
    public GameObject[] buildingPrefabs; // Liste des prefabs de bâtiments
    public int[] buildingCosts; // Coûts associés aux bâtiments
    private int selectedBuildingIndex = 0;

    public LayerMask placementLayer; // Assure-toi que le sol est dans ce Layer

    void Update()
    {
        // Empêche le placement si on clique sur un élément UI
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
            int cost = buildingCosts[selectedBuildingIndex];
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
