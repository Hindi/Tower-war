using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ProductInfo : MonoBehaviour
{
    [SerializeField]
    protected Text myName;
    public string Name
    {
        set { myName.text = value; }
        get { return myName.text; }
    }

    [SerializeField]
    protected Text price;
    public string Price
    {
        set { price.text = value; }
    }

    [SerializeField]
    protected Image image;
    public Sprite Image
    {
        set { image.sprite = value; }
        get { return image.sprite; }
    }

    protected int id;
    public int Id
    {
        set { id = value; }
    }
}
