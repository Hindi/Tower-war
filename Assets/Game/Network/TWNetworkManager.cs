using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using System.Collections;

public class TWNetworkManager : NetworkManager 
{
    [SerializeField]
    private ClientManager clientManager;

    [SerializeField]
    private Zone zone;
    
    public static NetworkClient Client;

    public static bool DEBUG = true;


    /* #######################################################
     * ######################## SERVER ####################### 
     * #######################################################*/
    void Awake()
    {
        Client = new NetworkClient();
	}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
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
    }

    public override void OnStartHost()
    {

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

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        TWClient client = clientManager.getClient(conn.connectionId);
        Vector3 pos = new Vector3(0, 0, 0);

        if(client != null)
            pos = new Vector3(client.id * 10, 0, 0);

        var obj = (GameObject)GameObject.Instantiate(playerPrefab, pos, Quaternion.identity);

        if (client != null)
        {
            client.gameObject = obj;
            clientManager.tryInitialyzeLifeCount();
        }
        NetworkServer.AddPlayerForConnection(conn, obj, playerControllerId);
    }

    /* #######################################################
     * ######################## CLIENT ####################### 
     * #######################################################*/
    public override void OnStartClient(NetworkClient client)
    {
        base.OnStartClient(client);
        Debug.Log("[Network MANAGER]Client created.");
        Client.RegisterHandler(TWNetworkMsg.start, OnStart);
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
        Client.Send(TWNetworkMsg.ready, msg);
    }
}
