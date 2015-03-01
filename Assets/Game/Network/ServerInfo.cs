using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ServerInfo : MonoBehaviour {

    [SerializeField]
    private Text serverName;
    public string ServerName
    {
        get { return serverName.text; }
        set { serverName.text = value; }
    }

    [SerializeField]
    private Text count;
    public string Count
    {
        get { return count.text; }
        set { count.text = value; }
    }

    [SerializeField]
    private Text max;
    public string Max
    {
        get { return max.text; }
        set { max.text = value; }
    }

    private UIServerList serverList;
    public UIServerList ServerList
    {
        get { return serverList; }
        set { serverList = value; }
    }

    public void onClick()
    {
        ServerList.select(this);
    }

}
