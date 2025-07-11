using UnityEngine;
using TMPro;
using UnityEngine.UI;

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
            
        }
        else
        {
           
            Debug.LogWarning("please enter a valid name.");
        }
    }
}
