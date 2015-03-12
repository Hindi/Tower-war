using UnityEngine;
using System.Collections;


public class Matchmaker : MonoBehaviour
{
    [SerializeField]
    bool connect;
    [SerializeField]
    bool gameDebug;

    [SerializeField]
    private FriendList friendList;

    // Use this for initialization
    void Start()
    {
        if (connect)
            PhotonNetwork.ConnectUsingSettings("0.1");
        else
        {
            PhotonNetwork.offlineMode = true;
            PhotonNetwork.CreateRoom("Boite");
        }
    }

    void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }

    void OnJoinedLobby()
    {
        if (gameDebug)
        {
            PhotonNetwork.JoinRandomRoom();
            UI.Instance.showGameUI();
        }
        else
        {
            UI.Instance.showServerList();
            UI.Instance.showFriendList();
            friendList.loadFriendList(1);
        }
    }

    void OnPhotonRandomJoinFailed()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.maxPlayers = 2;
        PhotonNetwork.JoinOrCreateRoom("Boite", roomOptions, TypedLobby.Default);
    }
}
