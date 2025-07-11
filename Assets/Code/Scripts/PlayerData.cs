using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class PlayerData
{
    public int money;
    public int totalBuildings;
    public Dictionary<string, int> buildingsByType;
    public List<string> purchasedUpgrades;
}
