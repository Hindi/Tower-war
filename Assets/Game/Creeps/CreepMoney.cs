using UnityEngine;
using System.Collections;

public class CreepMoney : MonoBehaviour {

    [SerializeField]
    private int price;
    public int Price
    {
        get { return price; }
        set { price = value; }
    }

    [SerializeField]
    private int incomeIncrease;
    public int IncomeIncrease
    {
        get { return incomeIncrease; }
        set { incomeIncrease = value; }
    }

    [SerializeField]
    private int moneyDrop;
    public int MoneyDrop
    {
        get { return moneyDrop; }
        set { moneyDrop = value; }
    }
}
