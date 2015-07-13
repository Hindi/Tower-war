using UnityEngine;
using System.Collections;

public class CreepMoney : MonoBehaviour {

    private int price;
    public int Price
    {
        get { return price; }
        set { price = value; }
    }

    private int incomeIncrease;
    public int IncomeIncrease
    {
        get { return incomeIncrease; }
        set { incomeIncrease = value; }
    }

    private int moneyDrop;
    public int MoneyDrop
    {
        get { return moneyDrop; }
        set { moneyDrop = value; }
    }
}
