using UnityEngine;
using System.Collections;

public class UI : MonoBehaviour {

    private static UI instance;

    public static UI Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType<UI>();
            return instance;
        }
    }

    [SerializeField]
    private UIBuildPopup buildPopup;
    [SerializeField]
    private UIUpgradePopup upgradePopup;
    [SerializeField]
    private UIBuyCreepsPopup buyCreepsPopup;
    [SerializeField]
    private UIServerList serverList;
    [SerializeField]
    private UIFriendList friendList;
    [SerializeField]
    private UIIncome incomeCanvas;
    public UIIncome IncomeCanvas
    { get { return incomeCanvas; } }

    public void showGameUI()
    {
        incomeCanvas.setActive(true);
    }

    public void showBuildPopup(Tile tile)
    {
        buildPopup.popUp(tile);
    }

    public void showUpgradePopupp(Tile tile)
    {
        upgradePopup.popUp(tile);
    }

    public void showBuyCreepsPopup(Vector3 pos)
    {
        buyCreepsPopup.popUp(pos);
    }

    public void showServerList()
    {
        serverList.setActive(true);
        serverList.generateServerList();
    }

    public void showFriendList()
    {
        friendList.setActive(true);
    }
}
