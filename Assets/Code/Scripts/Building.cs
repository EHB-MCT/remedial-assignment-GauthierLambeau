using UnityEngine;

public class Building : MonoBehaviour
{
    public int moneyPerSecond = 1;
    private float timer = 0f;

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

