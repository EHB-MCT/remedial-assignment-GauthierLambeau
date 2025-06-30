using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance;
    public int currentMoney = 0;
    public Text moneyText;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddMoney(int amount)
    {
        currentMoney += amount;
        moneyText.text = "Money: " + currentMoney;
    }

    public bool SpendMoney(int amount)
    {
        if (currentMoney >= amount)
        {
            currentMoney -= amount;
            moneyText.text = "Money: " + currentMoney;
            return true;
        }
        return false;
    }
}
