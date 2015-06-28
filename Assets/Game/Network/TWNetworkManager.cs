using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using System.Collections;

public class TWNetworkManager : NetworkManager 
{
    [SerializeField]
    private bool isServer;

    [SerializeField]
    private ClientManager clientManager;

    [SerializeField]
    private Zone zone;
    
    NetworkClient myClient;


    /* #######################################################
     * ######################## SERVER ####################### 
     * #######################################################*/
    void Start()
    {
        myClient = new NetworkClient();
	}


    public override void OnStartServer()
    {
        base.OnStartServer();
        Debug.Log("[Network MANAGER]Server created.");
        NetworkServer.RegisterHandler(TWNetworkMsg.ready, OnClientReady);
    }

    public override void OnServerConnect(NetworkConnection conn)
    {
        base.OnServerConnect(conn);
        Debug.Log("[NETWORK MANAGER]Client connected");
        clientManager.addNewClient(conn);
        zone.spawnTile(new Vector3(0, 0, 0));
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
    public override void OnStartClient(NetworkClient client)
    {
        base.OnStartClient(client);
        Debug.Log("[Network MANAGER]Client created.");
        myClient.RegisterHandler(TWNetworkMsg.start, OnStart);
    }

    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);
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
