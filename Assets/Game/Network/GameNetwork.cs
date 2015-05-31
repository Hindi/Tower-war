using UnityEngine;
using System.Collections;

public class GameNetwork : MonoBehaviour {

    [SerializeField]
    Zone zone;

    private bool firstPosition;

    private PhotonView photonView;

    void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    void OnJoinedRoom()
    {
        if (PhotonNetwork.player.isMasterClient)
        {
            spawnOnPosition(true);
            firstPosition = true;
        }
        else
            photonView.RPC("positionRequestRPC", PhotonTargets.Others);
    }

    void spawnOnPosition(bool b)
    {
        if (b)
            zone.spawnTile(Vector3.zero);
        else
            zone.spawnTile(new Vector3(10, 0, 0));
    }

    [RPC]
    public void positionAnswerRPC(bool isFirst)
    {
        firstPosition = isFirst;
        spawnOnPosition(isFirst);
    }

    [RPC]
    public void positionRequestRPC()
    {
        photonView.RPC("positionAnswerRPC", PhotonTargets.Others, !firstPosition);
    }
}
