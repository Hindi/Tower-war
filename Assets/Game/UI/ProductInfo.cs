using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ProductInfo : MonoBehaviour
{
    [SerializeField]
    protected Text name;
    public string Name
    {
        set { name.text = value; }
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
