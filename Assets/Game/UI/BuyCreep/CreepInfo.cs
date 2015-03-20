using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CreepInfo : MonoBehaviour {

    [SerializeField]
    private Text name;
    public string Name
    {
        set { name.text = value; }
    }

    [SerializeField]
    private Text price;
    public string Price
    {
        set { price.text = value; }
    }

    [SerializeField]
    private Text income;
    public string Income
    {
        set { income.text = value; }
    }

    [SerializeField]
    private Image image;
    public Sprite Image
    {
        set { image.overrideSprite = value; }
    }

    private UIBuyCreepsPopup buyCreepUI;
    public UIBuyCreepsPopup BuyCreepUI
    {
        set { buyCreepUI = value; }
    }

    private int id;
    public int Id
    {
        set { id = value; }
    }

    public void buyCreep()
    {
        buyCreepUI.buyCreep(id);
    }
}
