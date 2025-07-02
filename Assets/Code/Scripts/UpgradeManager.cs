using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UpgradeManager : MonoBehaviour
{
    [System.Serializable]
    public class Upgrade
    {
        public string upgradeName;
        public int cost;
        public float multiplier = 1.5f;
        public Button upgradeButton;
        public bool purchased = false;
    }

    public List<Upgrade> upgrades;
    public List<Building> allBuildings; 

    private void Start()
    {
        foreach (Upgrade upgrade in upgrades)
        {
            upgrade.upgradeButton.onClick.AddListener(() => ApplyUpgrade(upgrade));
        }
    }

    void ApplyUpgrade(Upgrade upgrade)
    {
        if (upgrade.purchased) return;

        if (MoneyManager.Instance.SpendMoney(upgrade.cost))
        {
            foreach (Building building in allBuildings)
            {
                building.moneyPerSecond = Mathf.RoundToInt(building.moneyPerSecond * upgrade.multiplier);
            }

            upgrade.purchased = true;
            upgrade.upgradeButton.interactable = false;
        }
    }
}
