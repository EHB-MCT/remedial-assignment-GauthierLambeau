using UnityEngine;
using Firebase.Firestore;
using System.Collections.Generic;
using System.Linq;

public class FirebaseSaver : MonoBehaviour
{
    public static FirebaseSaver Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void SavePlayerData(int money, Dictionary<string, int> buildingCounts, List<string> upgrades)
    {
        if (string.IsNullOrEmpty(PlayerLogin.PlayerName)) return;

        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        DocumentReference docRef = db.Collection("players").Document(PlayerLogin.PlayerName);

        Dictionary<string, object> data = new Dictionary<string, object>
        {
            { "money", money },
            { "buildings", buildingCounts },
            { "upgrades", upgrades }
        };

        docRef.SetAsync(data);
    }
    
    public Dictionary<string, int> CountBuildingsByType()
    {
        Dictionary<string, int> counts = new Dictionary<string, int>();
        foreach (var building in GameObject.FindObjectsOfType<Building>())
        {
            string type = building.buildingType;
            if (counts.ContainsKey(type)) counts[type]++;
            else counts[type] = 1;
        }
        return counts;
    }

    public List<string> GetPurchasedUpgrades()
    {
        List<string> upgrades = new List<string>();
        if (UpgradeManager.Instance != null)
        {
            foreach (var upgrade in UpgradeManager.Instance.upgrades)
                if (upgrade.purchased) upgrades.Add(upgrade.upgradeName);
        }
        return upgrades;
    }

    public void RestoreSessionFromSnapshot(DocumentSnapshot snapshot)
    {
        var data = snapshot.ToDictionary();


        int money = data.ContainsKey("money") ? System.Convert.ToInt32(data["money"]) : 100;
        MoneyManager.Instance.currentMoney = money;
        MoneyManager.Instance.SendMessage("UpdateMoneyUI");


        foreach (var building in Object.FindObjectsOfType<Building>())
            Destroy(building.gameObject);

        if (data.TryGetValue("buildings", out object buildingsObj) && buildingsObj is Dictionary<string, object> buildingsDict)
        {
            foreach (var kv in buildingsDict)
            {
                string buildingType = kv.Key;
                int count = System.Convert.ToInt32(kv.Value);
                for (int i = 0; i < count; i++)
                {
                    var placer = FindObjectOfType<BuildingPlacer>();
                    if (placer != null)
                        placer.ForceSpawnBuilding(buildingType);
                }
            }
        }

        if (data.TryGetValue("upgrades", out object upgradesObj) && upgradesObj is List<object> upgradesList)
        {
            foreach (string upgradeName in upgradesList.Cast<string>())
            {
                UpgradeManager.Instance.RestoreUpgrade(upgradeName);
            }
        }
    }
}
