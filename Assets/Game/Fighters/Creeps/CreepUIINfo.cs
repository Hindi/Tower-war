using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CreepUIINfo : MonoBehaviour {

    [SerializeField]
    private Sprite icon;
    public Sprite Icon
    {
        get { return icon; }
    }

    [SerializeField]
    public string Name
    {
        get { return gameObject.name; }
    }
}
