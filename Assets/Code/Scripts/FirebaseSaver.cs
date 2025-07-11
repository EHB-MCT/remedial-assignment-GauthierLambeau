using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;
using Firebase.Extensions;

public class FirebaseSaver : MonoBehaviour
{
    public static FirebaseSaver Instance;

    private FirebaseFirestore db;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            db = FirebaseFirestore.DefaultInstance;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SavePlayerData(int money, Dictionary<string, int> buildingsByType, List<string> purchasedUpgrades)
    {
        string playerId = PlayerPrefs.GetString("player_name", "test_player"); // Utilise le nom du joueur si disponible

        DocumentReference docRef = db.Collection("players").Document(playerId);

        Dictionary<string, object> data = new Dictionary<string, object>
        {
            { "money", money },
            { "buildings", buildingsByType },
            { "upgrades", purchasedUpgrades }
        };

        docRef.SetAsync(data).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted && !task.IsFaulted)
            {
                Debug.Log("<color=cyan>✅ Données sauvegardées dans Firestore !</color>");
            }
            else
            {
                Debug.LogError("❌ Erreur de sauvegarde Firestore : " + task.Exception);
            }
        });
    }

    public Dictionary<string, int> CountBuildingsByType()
    {
        Dictionary<string, int> buildingCounts = new Dictionary<string, int>();
        Building[] buildings = FindObjectsOfType<Building>();

        foreach (var building in buildings)
        {
            string name = building.name.Replace("(Clone)", "").Trim();

            if (buildingCounts.ContainsKey(name))
                buildingCounts[name]++;
            else
                buildingCounts[name] = 1;
        }

        return buildingCounts;
    }

    public List<string> GetPurchasedUpgrades()
    {
        List<string> purchased = new List<string>();
        UpgradeManager upgradeManager = FindObjectOfType<UpgradeManager>();

        if (upgradeManager != null)
        {
            foreach (var upg in upgradeManager.upgrades)
            {
                if (upg.purchased)
                {
                    purchased.Add(upg.upgradeName);
                }
            }
        }

        return purchased;
    }
}
