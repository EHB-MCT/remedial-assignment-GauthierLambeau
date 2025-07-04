using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UpgradeManager : MonoBehaviour
{
    public enum UpgradeType
    {
        GlobalMultiplier,
        BuildingMultiplier,
        CostReduction,
        InstantMoney
    }

    [System.Serializable]
    public class Upgrade
    {
        public string upgradeName;
        public int cost;
        public float multiplier = 1.5f;
        public Button upgradeButton;
        public bool purchased = false;
        public UpgradeType type;
        public string targetBuildingName;
        public int instantMoneyAmount;
        public int costReductionAmount;
    }

    public List<Upgrade> upgrades;
    public List<Building> allBuildings;

    public static int globalCostReduction = 0;

    private void Start()
    {
        foreach (Upgrade upgrade in upgrades)
        {
            Upgrade localUpgrade = upgrade;
            upgrade.upgradeButton.onClick.AddListener(() => ApplyUpgrade(localUpgrade));
        }
    }

    void ApplyUpgrade(Upgrade upgrade)
    {
        if (upgrade.purchased) return;

        if (MoneyManager.Instance.SpendMoney(upgrade.cost))
        {
            switch (upgrade.type)
            {
                case UpgradeType.GlobalMultiplier:
                    foreach (Building building in FindObjectsOfType<Building>())
                    {
                        building.moneyPerSecond = Mathf.RoundToInt(building.moneyPerSecond * upgrade.multiplier);
                    }
                    break;

                case UpgradeType.BuildingMultiplier:
                    foreach (Building building in FindObjectsOfType<Building>())
                    {

                        if (building.name.Contains(upgrade.targetBuildingName))
                        {
                            building.moneyPerSecond = Mathf.RoundToInt(building.moneyPerSecond * upgrade.multiplier);
                        }
                    }
                    break;

                case UpgradeType.CostReduction:
                    globalCostReduction += upgrade.costReductionAmount;
                    break;

                case UpgradeType.InstantMoney:
                    MoneyManager.Instance.AddMoney(upgrade.instantMoneyAmount);
                    break;
            }

            upgrade.purchased = true;
            upgrade.upgradeButton.interactable = false;
        }
    }


    public static int GetReducedCost(int baseCost)
    {
        int reducedCost = baseCost - globalCostReduction;
        return Mathf.Max(1, reducedCost);
    }


    public static void ApplyUpgradesToBuilding(Building building)
    {
        foreach (Upgrade upgrade in Instance.upgrades)
        {
            if (!upgrade.purchased) continue;

            switch (upgrade.type)
            {
                case UpgradeType.GlobalMultiplier:
                    building.moneyPerSecond = Mathf.RoundToInt(building.baseMoneyPerSecond * upgrade.multiplier);
                    break;

                case UpgradeType.BuildingMultiplier:
                    if (building.name.Contains(upgrade.targetBuildingName))
                    {
                        building.moneyPerSecond = Mathf.RoundToInt(building.baseMoneyPerSecond * upgrade.multiplier);
                    }
                    break;
            }
        }
    }

public static UpgradeManager Instance;

private void Awake()
{
    Instance = this;
}


}
