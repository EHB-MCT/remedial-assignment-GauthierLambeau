using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class IncomeDisplay : MonoBehaviour
{
    public TextMeshProUGUI incomeText;
    public List<Building> allBuildings;

    void Update()
{
    int totalIncome = 0;
    Building[] buildings = FindObjectsOfType<Building>();
    foreach (Building b in buildings)
    {
        totalIncome += b.moneyPerSecond;
    }
    incomeText.text = "Money/ sec : " + totalIncome;
}

}
