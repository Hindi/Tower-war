using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UIServerList : UIElement {

    [SerializeField]
    private Transform contentPanel;

    [SerializeField]
    private GameObject serverInfoPrefab;

    private ServerInfo selectedServer;
    private List<GameObject> serverList;

    [SerializeField]
    private Color notSelectedColor;
    [SerializeField]
    private Color selectedColor;
    [SerializeField]
    private Button joinButton;

	// Use this for initialization
	void Start () {
        serverList = new List<GameObject>();
	}

    void OnReceivedRoomListUpdate()
    {
        generateServerList();
    }

    public void generateServerList()
    {
        RoomInfo[] roomList = PhotonNetwork.GetRoomList();
        foreach(RoomInfo r in roomList)
        {
            if(r.visible && r.open)
            {
                ServerInfo si = ((GameObject)Instantiate(serverInfoPrefab)).GetComponent<ServerInfo>();
                si.ServerName = r.name;
                si.Count = r.playerCount.ToString();
                si.Max = r.maxPlayers.ToString();
                si.ServerList = this;
                serverList.Add(si.gameObject);
            }
        }

        showServerList();
    }

    private void showServerList()
    {
        foreach (GameObject o in serverList)
            o.transform.SetParent(contentPanel);
    }

    public void select(ServerInfo selected)
    {
        if (selected == selectedServer)
        {
            selectedServer.gameObject.GetComponent<Image>().color = notSelectedColor;
            selectedServer = null;
        }
        else
        {
            if (selectedServer != null)
                selectedServer.gameObject.GetComponent<Image>().color = notSelectedColor;
            selectedServer = selected;
            selectedServer.gameObject.GetComponent<Image>().color = selectedColor;
        }
        joinButton.interactable = (selectedServer != null);
    }

    public void join()
    {
        PhotonNetwork.JoinRoom(selectedServer.ServerName);
    }
}
