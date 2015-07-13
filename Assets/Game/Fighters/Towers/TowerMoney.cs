using UnityEngine;
using System.Collections;

public class TowerMoney : MonoBehaviour 
{
    private int price;
    public int Price
    {
        get { return price; }
        set { price = value; }
    }

    private int upgradePrice;
    public int UpgradePrice
    {
        get { return upgradePrice; }
        set { upgradePrice = value; }
    }
}
