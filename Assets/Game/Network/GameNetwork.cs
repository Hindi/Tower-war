using UnityEngine;
using System.Collections;

public class GameNetwork : MonoBehaviour {

    [SerializeField]
    Zone zone;

    void OnJoinedRoom()
    {
        if (PhotonNetwork.player.isMasterClient)
        {
            zone.spawnTile(Vector3.zero);
        }
        else
            zone.spawnTile(new Vector3(0, 7, 0));
        EventManager.Raise(EnumEvent.START);
    }
}
