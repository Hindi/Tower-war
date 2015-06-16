using UnityEngine;
using System.Collections;

public class ProdutUIInfo : MonoBehaviour
{
    [SerializeField]
    private Sprite icon;
    public Sprite Icon
    {
        get { return icon; }
    }

    [SerializeField]
    private string name;
    public string Name
    {
        get { return gameObject.name; }
    }
}
