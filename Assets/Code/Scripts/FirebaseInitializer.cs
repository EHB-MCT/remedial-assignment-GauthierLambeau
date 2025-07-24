using UnityEngine;
using Firebase;
using Firebase.Extensions;
using Firebase.Firestore;
using System.Collections.Generic;

public class FirebaseInitializer : MonoBehaviour
{
    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            var status = task.Result;
            if (status == DependencyStatus.Available)
            {
                var app = FirebaseApp.DefaultInstance;
                Debug.Log("<color=green>✅ Firebase is initialised !</color>");

                WriteTestDataToFirestore();
            }
            else
            {
                Debug.LogError("❌ Firebase not available : " + status.ToString());
            }
        });
    }

    void WriteTestDataToFirestore()
    {
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;

        DocumentReference docRef = db.Collection("players").Document("test_player");

        Dictionary<string, object> testData = new Dictionary<string, object>
        {
            { "money", 100 },
            { "buildings", 3 },
            { "upgrades", new List<string> { "double_income", "cost_reduction" } }
        };

        docRef.SetAsync(testData).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted && !task.IsFaulted)
            {
                Debug.Log("<color=cyan>✅ Test data saved inside Firestore !</color>");
            }
            else
            {
                Debug.LogError("❌ Error with the Firestore : " + task.Exception);
            }
        });
    }
}
