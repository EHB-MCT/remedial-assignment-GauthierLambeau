using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Firebase.Firestore;
using Firebase.Extensions;

public class PlayerLogin : MonoBehaviour
{
    public TMP_InputField pseudoInput;
    public Button confirmButton;
    public GameObject loginPanel; 
    public static string PlayerName { get; private set; }

    void Start()
    {
        confirmButton.onClick.AddListener(OnConfirm);
        loginPanel.SetActive(true); 
    }

    void OnConfirm()
    {
        string pseudo = pseudoInput.text.Trim();
        if (!string.IsNullOrEmpty(pseudo))
        {
            PlayerName = pseudo;
            loginPanel.SetActive(false);

            FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
            var docRef = db.Collection("players").Document(PlayerName);
            docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
            {
                if (task.Result.Exists)
                {
                  
                    FirebaseSaver.Instance.RestoreSessionFromSnapshot(task.Result);
                }
                else
                {
                    MoneyManager.Instance.currentMoney = 100; 
                    MoneyManager.Instance.SendMessage("UpdateMoneyUI");
                }
            });
        }
        else
        {
            Debug.LogWarning("please enter a valid name.");
        }
    }
}
