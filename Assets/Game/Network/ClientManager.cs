using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using System.Collections;
using System.Collections.Generic;

public class TWClient
{
    public NetworkConnection connection;
    public GameObject gameObject;
    public bool isConnected;
    public bool isReady;
    public int id;

    public TWClient() { isConnected = true; }
    public TWClient(NetworkConnection c) 
    { 
        connection = c;
        isConnected = true;
    }
}

public class ClientManager : MonoBehaviour 
{
    [SerializeField]
    private int maxClientCount;

    private Dictionary<int, TWClient> clients; 

    TWClient client1;
    TWClient client2;

    void Start()
    {
        clients = new Dictionary<int, TWClient>();
    }

    public void addNewClient(NetworkConnection connection)
    {
        TWClient twc = new TWClient(connection);
        if (!clients.ContainsKey(connection.connectionId))
            clients.Add(connection.connectionId, twc);

        if (client1 == null)
        {
            client1 = twc;
            client1.id = 0;
        }
        else
        {
            client2 = twc;
            client2.id = 1;
        }
    }

    public void tryInitialyzeLifeCount()
    {
        if(client2 != null)
        {
            var lc1 = client1.gameObject.GetComponent<LifeCount>();
            var lc2 = client2.gameObject.GetComponent<LifeCount>();

            lc1.initEnemyLifeCount(lc2);
            lc2.initEnemyLifeCount(lc1);
        }
    }

    public bool isRoomFull()
    {
        return clients.Count >= maxClientCount;
    }

    public bool allClientReady()
    {
        return client1.isReady && client2.isReady;
    }

    public TWClient getClient(int id)
    {
        return clients[id];
    }

    public void sendStart()
    {
        var msg = new EmptyMessage();
        client1.connection.Send(TWNetworkMsg.start, msg);
        client2.connection.Send(TWNetworkMsg.start, msg);
    }
}
