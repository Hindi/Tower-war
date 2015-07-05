using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIFriendInfo : MonoBehaviour
{
    [SerializeField]
    private Text myName;
    public string Name
    {
        get { return myName.text; }
        set { myName.text = value; }
    }

    [SerializeField]
    private Text level;
    public string Level
    {
        get { return level.text; }
        set { level.text = value; }
    }
}
