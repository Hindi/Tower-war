/**************************************************************************************
 * Defines                                                                            *
 **************************************************************************************/

using UnityEngine;
using System;
using System.Net;
using System.Text;
using System.Collections;
using System.Threading;
using System.Net.Sockets;
using System.Collections.Generic;

/// <summary>
/// C3PONetwork is a class facilitating network communication for the C3PO project.
/// Only basic networking is managed here.
///  This class is built against the singleton model</summary>
public class MyNetwork : MonoBehaviour
{
    /// <summary>Set to true or false in Unity before compiling.</summary>
    [SerializeField]
    private static bool isServerLaunch;
    public static bool IsServerLaunch
    {
        get { return isServerLaunch; }
    }
    /*
    private bool isServer;
    public bool IsServer
    {
        get { return Network.isServer; }
    }

    // <summary>MasterPort is the port number on which the MasterServer is set.</summary>
    [SerializeField]
    private int masterPort = 23466;

    // <summary>GamePort number is the port on which the game server is set.</summary>
    [SerializeField]
    private int gamePort = 25000;

    // <summary>Server lists received from MasterServer for both Teacher Server and match making.</summary>
    private HostData[] hostList = null;

    public static string serverName;

    // <summary>Object that does the detection of the game server.</summary>
    AutomaticConnectionManager automaticConnectionManager;

    /// <summary>Dictionnaire contenant un identifiant unique pou chaque client.</summary>
    private Dictionary<string, Client> clientsInfos = null;
    public Dictionary<string, Client> ClientsInfos
    {
        get { return clientsInfos; }
    }

    /// <summary>Object that manages the players credentials.</summary>
    PlayerCredential playerCredentials;

    /// <summary>The current course id.</summary>
    private int currentCourseId;

    /// <summary>The login of the client.</summary>
    public string login;
    /// <summary>The password of the client.</summary>
    public string password;

    /// <summary>Referenceto the level loader.</summary>
    [SerializeField]
    private LevelLoader levelLoader;

    /// <summary>Reference to the state manager.</summary>
    [SerializeField]
    private StateManager stateManager;

    [SerializeField]
    private ServerMenu serverMenu;

    // <summary>Inner class made for the detection of the server.</summary>
    public class AutomaticConnectionManager
    {
        // <summary>Object that broadcasts the ip.</summary>
        public Sender ipSender;

        // <summary>The udp socket.</summary>
        private UdpClient udp;

        // <summary>True if it's the server.</summary>
        private bool isServer;

        // <summary>Constructor.</summary>
        /// <param name="server">True if it's the server.</param>
        public AutomaticConnectionManager(bool server)
        {
            isServer = server;
            udp = new UdpClient(15000);
            ipSender = new Sender(udp);
        }

        // <summary>CStore the server ip as a string.</summary>
        private string serverIp = "";
        public string ServerIp
        {
            get { return serverIp; }
            set { serverIp = value; }
        }

        // <summary>True if the ip has been recieved.</summary>
        private bool received = false;
        public bool Received
        {
            get { return received; }
            set { received = value; }
        }

        // <summary>Starts the coroutine that listen for udp broadcast.</summary>
        public void StartListening()
        {
            this.udp.BeginReceive(Receive, new object());
        }

        // <summary>The function used by the coroutine to receive the server's ip.</summary>
        private void Receive(IAsyncResult ar)
        {
            try
            {
                IPEndPoint ip = new IPEndPoint(IPAddress.Any, 15000);
                byte[] bytes = udp.EndReceive(ar, ref ip);
                string message = Encoding.ASCII.GetString(bytes);
                if (!isServer)
                {
                    if (message.Split(' ')[0] == serverName)
                    {
                        serverIp = message.Split(' ')[1];
                        received = true;
                    }
                    else
                        StartListening();
                }
            }
            catch (Exception ex)
            {
                Debug.Log("EndReceive failed : " + ex);
            }
        }
    }

    // <summary>Inner class that boradcast the server's ip on the local network.</summary>
    public class Sender
    {
        // <summary>Stores the server IP as a string.</summary>
        private string serverIp;
        public string ServerIp
        {
            get { return serverIp; }
            set { serverIp = value; }
        }

        // <summary>The udp socket.</summary>
        UdpClient udp;

        // <summary>Stores the server IP as an IP object.</summary>
        IPEndPoint ip;

        // <summary>Constructor.</summary>
        /// <param name="udp">Udp socket used to broadcast on local network.</param>
        public Sender(UdpClient udp)
        {
            this.udp = udp;
            serverIp = localIPAddress();
            ip = new IPEndPoint(IPAddress.Broadcast, 15000);
        }

        // <summary>Finds and returns the local ipAdress.</summary>
        private string localIPAddress()
        {
            IPHostEntry host;
            string localIP = "";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                    break;
                }
            }
            return localIP;
        }

        // <summary>Broadcast the server's ip.</summary>
        public void sendIp(string ip)
        {
            broadCastSomething(serverName + " " + ip);
        }

        // <summary>Broadcast a string on the network.</summary>
        public void broadCastSomething(string s)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(s);
            udp.Send(bytes, bytes.Length, ip);
        }
    }
    
    /// <summary>Functions used to connect to the server application-wise .</summary>
    /// <param name="ip">The ip of the server.</param>
    /// <param name="login">The login of the client.</param>
    /// <param name="password">The password of the client.</param>
    /// <returns>void</returns>
    public void connectToServer(string ip, string login, string password)
    {
        this.login = login;
        this.password = password;

        connectServer(ip);
    }

    /// <summary>Functions used to sign in on the server.</summary>
    /// <param name="login">The login of the client.</param>
    /// <param name="password">The password of the client.</param>
    /// <returns>void</returns>
    public void tryTologIn(string login, string password)
    {
        this.login = login;
        this.password = password;
        tryTologIn();
    }

    /// <summary>Functions used to connect to the teacher server application-wise .</summary>
    /// <returns>void</returns>
    public void tryTologIn()
    {
        if (!Network.isClient)
        {
            throw new System.Exception("Failed to connect to teacher");
        }

        networkView.RPC("clientConnect", RPCMode.Server, login, password);
    }

    /// <summary>Called when the authentication failed and broadcast the reason with the event manager.</summary>
    /// <param name="reason">The reason why the authentication failed.</param>
    /// <returns>void</returns>
    public void onFailedAuth(string reason)
    {
        EventManager<string>.Raise(EnumEvent.AUTHFAILED, reason);
    }

    /// <summary>Unity function called when a player is disconnected. When this happens, save the player's stats and clear the lists</summary>
    /// <param name="client">Unity object that contains all the network related infos of the client.</param>
    /// <returns>void</returns>
    void OnPlayerDisconnected(NetworkPlayer client)
    {
        foreach (KeyValuePair<string, Client> e in clientsInfos)
        {
            if (e.Value.NetworkPlayer == client)
            {
                e.Value.saveStats(currentCourseId);
                clientsInfos.Remove(e.Key);
                return;
            }
        }
    }

    /// <summary>Kick a specific client.</summary>
    /// <param name="login">The login of the client.</param>
    /// <returns>void</returns>
    public void kickClient(string login)
    {
        foreach (KeyValuePair<string, Client> e in clientsInfos)
            if (e.Value.Login == login)
                Network.CloseConnection(e.Value.NetworkPlayer, true);
    }

    /// <summary>Kick all the connected clients.</summary>
    /// <returns>void</returns>
    public void kickClient()
    {
        foreach (KeyValuePair<string, Client> e in clientsInfos)
            Network.CloseConnection(e.Value.NetworkPlayer, true);
    }

    /// <summary>Reset the password of all the connected clients.</summary>
    /// <returns>void</returns>
    public void resetPassword()
    {
        playerCredentials.resetPassword();
    }

    /// <summary>Reset the password of a specific client.</summary>
    /// <param name="login">The login of the client.</param>
    /// <returns>void</returns>
    public void resetPassword(string login)
    {
        playerCredentials.resetPassword(login);
    }


    /// <summary>Function to be called on the teacher version of the program.
    /// Launch and initialize the  master server and register the server.</summary>
    /// <returns>void</returns>
    public void createServer()
    {
        System.Diagnostics.Process.Start(Application.dataPath + @"\Resources\MasterServer\MasterServer.exe");

        MasterServer.port = masterPort;
        MasterServer.ipAddress = getMyIp(); 
        Network.InitializeServer(1000, gamePort, false);
        MasterServer.RegisterHost(serverName, "");
    }

    /// <summary>Connects to the MasterServer to find the game server and connects to it.</summary>
    /// <param name="ip">The ip of the server.</param>
    /// <returns>void</returns>
    public void connectServer(string ip)
    {
        MasterServer.port = masterPort;
        MasterServer.ipAddress = ip;
        MasterServer.RequestHostList(serverName);
    }

    /// <summary>Called when the client recieves the server's ip and stores it.</summary>
    /// <param name="ip">The ip of the server.</param>
    /// <returns>void</returns>
    void onServerIpRecieved(string ip)
    {
        automaticConnectionManager.ServerIp = ip;
    }

    /// <summary>Uses the ipSender to broadcast the local ip address.</summary>
    /// <returns>void</returns>
    void sendIp()
    {
        automaticConnectionManager.ipSender.sendIp(getMyIp());
    }

    /// <summary>This is where we differenciate server and client. The first broadcast the ip and the other tries to catch it.</summary>
    /// <returns>void</returns>
    void Start()
    {
        serverName = "Server";
        if (IS_SERVER)
        {
            automaticConnectionManager = new AutomaticConnectionManager(true);
            InvokeRepeating("sendIp", 0, 1);
            clientsInfos = new Dictionary<string, Client>();
            playerCredentials = new PlayerCredential();
        }
        else
        {
            automaticConnectionManager = new AutomaticConnectionManager(false);
            EventManager<string>.AddListener(EnumEvent.SERVERIPRECEIVED, onServerIpRecieved);
            automaticConnectionManager.StartListening();
        }
        currentCourseId = 0;
    }

    /// <summary>Get the ip address of the machine.</summary>
    /// <returns>string : the ip address</returns>
    public string getMyIp()
    {
        return automaticConnectionManager.ipSender.ServerIp;
    }

    /// <summary>Get the ip address of the server.</summary>
    /// <returns>void</returns>
    public string getServerIp()
    {
        if (automaticConnectionManager.ServerIp != null)
            return automaticConnectionManager.ServerIp;
        else
            return "";
    }

    /// <summary>Called when connected to server, tries to login.</summary>
    /// <returns>void</returns>
    void OnConnectedToServer()
    {
        EventManager.Raise(EnumEvent.CONNECTIONTOUNITY);
        tryTologIn();
    }

    /// <summary>Called when the client is disconnected from the server. Broadcast the info with events.</summary>
    /// <param name="info">Unity object that contains the disconnection info.</param>
    /// <returns>void</returns>
    void OnDisconnectedFromServer(NetworkDisconnection info)
    {
        EventManager.Raise(EnumEvent.DISCONNECTFROMUNITY);
    }

    /// <summary>If the IP/Hostname is not correct or the Network is down notice the client.</summary>
    /// <returns>void</returns>
    void OnFailedToConnectToMasterServer()
    {
        onFailedAuth("Cannot find the server.");
    }

    /// <summary>Called when the master server sends the host list. Connects to the server that name is "C3PO"</summary>
    /// <param name="msEvent">Unity object that contains the list of the servers connected to the master server.</param>
    /// <returns>void</returns>
    void OnMasterServerEvent(MasterServerEvent msEvent)
    {
        if (msEvent == MasterServerEvent.HostListReceived)
        {
            hostList = MasterServer.PollHostList();
            if (hostList.Length > 0 && hostList[0].gameType == serverName)
            {
                Network.Connect(hostList[0]);
            }
        }
    }

    public void disconect()
    {
        Network.Disconnect();
    }

    /// <summary>Functions used to broadcast to all the client the order to load a unity scene.</summary>
    /// <param name="name">The name of the scene.</param>
    /// <returns>void</returns>
    public void loadLevel(string name)
    {
        int questionCount = QuestionManager.Instance.CurrentQuestionNb;
        float goodAnswerRatio = 0;

        foreach (KeyValuePair<string, Client> p in clientsInfos)
        {
            if (questionCount != 0)
                goodAnswerRatio = (float)((float)p.Value.Score / (float)questionCount);
            else
                goodAnswerRatio = 0;
            networkView.RPC("rpcLoadLevel", p.Value.NetworkPlayer, name, goodAnswerRatio);
        }
    }

    /// <summary>Functions used to notify the player he won or lost his game.</summary>
    /// <param name="b">True if won.</param>
    /// <returns>void</returns>
    public void sendNotifyGameOver(NetworkPlayer netPlayer, bool b)
    {
        networkView.RPC("notifyGameOver", netPlayer, b);
    }

    /// <summary>Functions used to notify a specific client that the login he uses does not exists.</summary>
    /// <param name="netPlayer">Unity object containing the client's info (used by the RPC).</param>
    /// <param name="name">The login.</param>
    /// <returns>void</returns>
    public void sendNotifyWrongLogin(NetworkPlayer netPlayer, string name)
    {
        networkView.RPC("notifyWrongLogin", netPlayer, name);
    }

    /// <summary>Functions used to notify a specific client that the password he uses is not correct.</summary>
    /// <param name="netPlayer">Unity object containing the client's info (used by the RPC).</param>
    /// <param name="name">The login.</param>
    /// <returns>void</returns>
    public void sendNotifyWrongPassword(NetworkPlayer netPlayer, string name)
    {
        networkView.RPC("notifyWrongPassword", netPlayer, name);
    }

    /// <summary>Functions used to notify a specific client that the login he use is already used.</summary>
    /// <param name="netPlayer">Unity object containing the client's info (used by the RPC).</param>
    /// <param name="name">The login.</param>
    /// <returns>void</returns>
    public void sendNotifyLoginInUse(NetworkPlayer netPlayer, string login)
    {
        networkView.RPC("notifyLoginInUse", netPlayer, login);
    }

    /// <summary>Checks weither this login is already used or not.</summary>
    /// <param name="login">The login to be checked.</param>
    /// <returns>bool : True if the login is already in use.</returns>
    private bool loginInUse(string login)
    {
        foreach (KeyValuePair<string, Client> p in clientsInfos)
        {
            if (p.Value.Login == login)
                return true;
        }
        return false;
    }

    /// <summary>RPC : sign in request on the server.</summary>
    /// <param name="login">The login of the client.</param>
    /// <param name="password">The password of the client.</param>
    /// <param name="info">Unity object containing the infos on the client.</param>
    /// <returns>void</returns>
    [RPC]
    void clientConnect(string login, string password, NetworkMessageInfo info)
    {
        if (playerCredentials.checkAuth(login, password, info.sender))
        {
            if (loginInUse(login))
            {
                sendNotifyLoginInUse(info.sender, login);
            }
            else
            {
                networkView.RPC("clientSuccessfullyConnected", info.sender);
                Client c = new Client();
                c.Login = login;
                string id = login + System.DateTime.Now;
                c.Id = id;
                c.NetworkPlayer = info.sender;
                clientsInfos.Add(id, c);
                c.loadStats(currentCourseId);
            }
        }
    }

    /// <summary>RPC : notify the client he is authenticated. Sends back a unique Id to this client.</summary>
    /// <param name="uniqueID">The uniqueID of the client.</param>
    /// <returns>void</returns>
    [RPC]
    void clientSuccessfullyConnected()
    {
        EventManager.Raise(EnumEvent.AUTHSUCCEEDED);
    }

    /// <summary>RPC : order the client to load a unity scene.</summary>
    /// <param name="question">The question asked.</param>
    /// <param name="level">The level name.</param>
    /// <param name="goodAnswerRatio">The ratio that dtermines how the student succeded during thequestion / answer phase.</param>
    /// <returns>void</returns>
    [RPC]
    void rpcLoadLevel(string level, float goodAnswerRatio)
    {
        QuestionManager.Instance.AnswerRatio = goodAnswerRatio;
        levelLoader.loadLevel(level);
    }

    /// <summary>RPC : notify the client he used a wrong login.</summary>
    /// <param name="login">The login of the client.</param>
    /// <returns>void</returns>
    [RPC]
    void notifyWrongLogin(string login)
    {
        onFailedAuth("Wrong login : " + login);
    }

    /// <summary>RPC : notify the client he used a wrong password.</summary>
    /// <param name="pass">The pass of the client. Not used for now.</param>
    /// <returns>void</returns>
    [RPC]
    void notifyWrongPassword(string pass)
    {
        onFailedAuth("Wrong password.");
    }

    /// <summary>RPC : notify the client his login is already in use.</summary>
    /// <param name="login">The login of the client.</param>
    /// <returns>void</returns>
    [RPC]
    void notifyLoginInUse(string login)
    {
        onFailedAuth("Login " + login + " already in use.");
    }

    /// <summary>Notify the server that the player won or lost his game.</summary>
    /// <param name="uniqueID">The uniqueID of the client.</param>
    /// <param name="gameId">The id of the current game.</param>
    /// <param name="b">True if won.</param>
    /// <returns>void</returns>
    [RPC]
    void notifyGameOver(bool b)
    {
        Debug.Log("Game  over " + b);
    }*/
}
