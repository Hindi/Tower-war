using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using System.Collections;

public class TWNetworkManager : NetworkManager 
{
    [SerializeField]
    private bool isServer;
    [SerializeField]
    private GameObject obj;

    [SerializeField]
    private ClientManager clientManager;

    [SerializeField]
    private Zone zone;
    
    NetworkClient myClient;


    /* #######################################################
     * ######################## SERVER ####################### 
     * #######################################################*/
	void Start () {
        if(isServer)
        {
            StartHost();
            NetworkServer.Listen(4444);
            NetworkServer.RegisterHandler(TWNetworkMsg.ready, OnClientReady);
            NetworkServer.RegisterHandler(MsgType.Connect, OnClientConnected);
        }
        else
        {
            StartClient();
            myClient = new NetworkClient();
            myClient.RegisterHandler(MsgType.Connect, OnConnected);
            myClient.RegisterHandler(TWNetworkMsg.start, OnStart);
            myClient.Connect("127.0.0.1", 4444);
        }
	}

    public void OnClientConnected(NetworkMessage netMsg)
    {
        Debug.Log("[NETWORK MANAGER]Client connected");
        clientManager.addNewClient(netMsg.conn);
        GameObject gobj = (GameObject)Instantiate(obj);
        NetworkServer.Spawn(gobj);
        if (clientManager.isRoomFull())
            StartCoroutine(waitClientsReady());
        //zone.spawnTile(new Vector3(0, 0, 0));
    }

    private IEnumerator waitClientsReady()
    {
        while(!clientManager.allClientReady())
        {
            yield return null;
        }
        Debug.Log("[NETWORK MANAGER]Both clients are ready");
        clientManager.sendStart();
        EventManager.Raise(EnumEvent.START);
    }
    
    void OnClientReady(NetworkMessage netMsg)
    {
        clientManager.getClient(netMsg.conn.connectionId).isReady = true;
    }

    /* #######################################################
     * ######################## CLIENT ####################### 
     * #######################################################*/
    public void OnConnected(NetworkMessage netMsg)
    {
        Debug.Log("[NETWORK MANAGER]Connected to server");
        StartCoroutine(simulateLoadingCoroutine());
    }

    public void OnStart(NetworkMessage netMsg)
    {
        Debug.Log("[NETWORK MANAGER]Game starts");
    }

    private IEnumerator simulateLoadingCoroutine()
    {
        yield return new WaitForSeconds(1);
        sendReady();
    }

    private void sendReady()
    {
        var msg = new EmptyMessage();
        myClient.Send(TWNetworkMsg.ready, msg);
    }
    
}
