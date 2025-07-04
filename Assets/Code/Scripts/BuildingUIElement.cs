using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildingUIElement : MonoBehaviour
{
    public string buildingName;
    public int baseCost;
    public TextMeshProUGUI costText;

    void Start()
    {
        UpdateCostDisplay();
    }

   public void UpdateCostDisplay()
{
    int reducedCost = UpgradeManager.GetReducedCost(baseCost);
    costText.text = buildingName + " - " + reducedCost + "$";
    Debug.Log("Updated cost for " + buildingName + ": " + reducedCost);
}

}
