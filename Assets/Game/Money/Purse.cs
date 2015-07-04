using UnityEngine;
using System.Collections;

public class Purse : MonoBehaviour {

    private int currentAmount;

    [SerializeField]
    private int startAmount;

    [SerializeField]
    private UIIncome incomeCanvas;

    void Start()
    {
        currentAmount = startAmount;
        updateUI();
    }

    private void updateUI()
    {
        incomeCanvas.MoneyAmount = currentAmount.ToString();
    }

    public void add(int amount)
    {
        currentAmount += amount;
        updateUI();
    }

    public bool canAfford(int amount)
    {
        return amount <= currentAmount;
    }

    public void substract(int amount)
    {
        currentAmount -= amount;
        updateUI();
    }
}
