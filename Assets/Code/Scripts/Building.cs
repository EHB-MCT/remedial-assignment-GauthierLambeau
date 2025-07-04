using UnityEngine;
public class Building : MonoBehaviour
{
    public int baseMoneyPerSecond = 1;
    public int moneyPerSecond;

    private float timer = 0f;

    void Awake()
{
    moneyPerSecond = baseMoneyPerSecond;

    if (UpgradeManager.Instance != null)
    {
        UpgradeManager.ApplyUpgradesToBuilding(this);
    }
}


    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 1f)
        {
            MoneyManager.Instance.AddMoney(moneyPerSecond);
            timer = 0f;
        }
    }
}


