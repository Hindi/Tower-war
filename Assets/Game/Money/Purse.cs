using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Purse : NetworkBehaviour {

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
        RpcAdd(amount);
    }

    [ClientRpc]
    public void RpcAdd(int amount)
    {
        if(!isClient)
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
        RpcSubstract(amount);
    }

    [ClientRpc]
    public void RpcSubstract(int amount)
    {
        if (!isClient)
            currentAmount -= amount;
        updateUI();
    }
}
