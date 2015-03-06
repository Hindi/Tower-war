using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIFriendInfo : MonoBehaviour
{
    [SerializeField]
    private Text name;
    public string Name
    {
        get { return name.text; }
        set { name.text = value; }
    }

    [SerializeField]
    private Text level;
    public string Level
    {
        get { return level.text; }
        set { level.text = value; }
    }
}
