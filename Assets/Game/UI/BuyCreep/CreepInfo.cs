using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CreepInfo : ProductInfo
{
    [SerializeField]
    protected Text income;
    public string Income
    {
        set { income.text = value; }
    }

    private UIBuyCreepsPopup buyCreepUI;
    public UIBuyCreepsPopup BuyCreepUI
    {
        set { buyCreepUI = value; }
    }

    public void buyCreep()
    {
        buyCreepUI.buyCreep(id);
    }
}
