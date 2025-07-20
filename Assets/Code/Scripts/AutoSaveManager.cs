using UnityEngine;

public class AutoSaveManager : MonoBehaviour
{
    private bool hasSaved = false;

    void OnApplicationQuit()
    {
        SaveBeforeExit();
    }

    public void SaveBeforeExit()
    {
        if (hasSaved) return;

        if (FirebaseSaver.Instance != null && MoneyManager.Instance != null)
        {
            int currentMoney = MoneyManager.Instance.currentMoney;

            var buildings = FirebaseSaver.Instance.CountBuildingsByType();
            var upgrades = FirebaseSaver.Instance.GetPurchasedUpgrades();

            FirebaseSaver.Instance.SavePlayerData(currentMoney, buildings, upgrades);

            hasSaved = true;
        }
        else
        {
            Debug.LogWarning("FirebaseSaver or MoneyManager not found.");
        }
    }
}
