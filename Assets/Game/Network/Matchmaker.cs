using UnityEngine;
using System.Collections;


public class Matchmaker : MonoBehaviour
{
    [SerializeField]
    bool connect;
    [SerializeField]
    bool gameDebug;

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
            PhotonNetwork.JoinRandomRoom();
        else
            UI.Instance.showServerList();
    }

    void OnPhotonRandomJoinFailed()
    {
        PhotonNetwork.CreateRoom("Boite");
    }
}
