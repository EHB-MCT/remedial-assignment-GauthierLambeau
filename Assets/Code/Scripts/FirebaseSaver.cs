using UnityEngine;
using Firebase.Firestore;
using System.Collections.Generic;

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
        if (string.IsNullOrEmpty(PlayerLogin.PlayerName))
        {
            Debug.LogWarning("⛔ Aucun nom de joueur défini. Impossible de sauvegarder.");
            return;
        }

        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        DocumentReference docRef = db.Collection("players").Document(PlayerLogin.PlayerName);

        Dictionary<string, object> data = new Dictionary<string, object>
        {
            { "money", money },
            { "buildings", buildingCounts },
            { "upgrades", upgrades }
        };

        docRef.SetAsync(data).ContinueWith(task =>
        {
            if (task.IsCompleted && !task.IsFaulted)
            {
                Debug.Log("<color=green>✅ Sauvegarde réussie pour " + PlayerLogin.PlayerName + "</color>");
            }
            else
            {
                Debug.LogError("❌ Erreur lors de la sauvegarde : " + task.Exception);
            }
        });
    }

    // Simulé pour l’exemple : à adapter selon ton jeu
    public Dictionary<string, int> CountBuildingsByType()
    {
        Dictionary<string, int> example = new Dictionary<string, int>
        {
            { "farm", 3 },
            { "factory", 1 }
        };
        return example;
    }

    public List<string> GetPurchasedUpgrades()
    {
        return new List<string> { "double_income", "cost_reduction" };
    }
}
