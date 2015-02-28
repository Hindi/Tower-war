using UnityEngine;
using System.Collections;


public class Matchmaker : MonoBehaviour
{
    [SerializeField]
    bool connect;
    [SerializeField]
    Zone zone;

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

    void OnJoinedRoom()
    {
        if (PhotonNetwork.playerList.Length == 1)
            zone.spawnTile(Vector3.zero);
        else
            zone.spawnTile(new Vector3(0, 7, 0));
        EventManager.Raise(EnumEvent.START);
    }

    void OnPhotonRandomJoinFailed()
    {
        PhotonNetwork.CreateRoom("Boite");
    }
}
