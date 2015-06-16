using UnityEngine;
using System.Collections;

public class TowerInfo : ProductInfo
{
    private UIBuildPopup buyTowerUI;
    public UIBuildPopup BuyTowerUI
    {
        set { buyTowerUI = value; }
    }

    public void buy()
    {
        buyTowerUI.build(id);
    }
}
