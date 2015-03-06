using UnityEngine;
using System.Collections;

public class Purse : MonoBehaviour {

    private int currentAmount;

    [SerializeField]
    private int startAmount;

    void Start()
    {
        currentAmount = startAmount;
    }

    public void add(int amount)
    {
        currentAmount += amount;
    }

    public bool canAfford(int amount)
    {
        return amount < currentAmount;
    }

    public void substract(int amount)
    {
        if (canAfford(amount))
            currentAmount -= amount;
    }
}
