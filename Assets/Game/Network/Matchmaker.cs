using UnityEngine;
using System.Collections;


public class Matchmaker : MonoBehaviour
{
    [SerializeField]
    bool connect;

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
        PhotonNetwork.JoinRandomRoom();
    }

    void OnPhotonRandomJoinFailed()
    {
        PhotonNetwork.CreateRoom("Boite");
    }
}
