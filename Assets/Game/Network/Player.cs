using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Player : NetworkBehaviour
{
    [SerializeField]
    private Zone zone;

    [Command]
    public void CmdRequestBuild(int tileId, int towerId)
    {
        Debug.Log("Request build on " + tileId + " with tower " + towerId);
        if (zone.canBuildHere(tileId))
        {
            if (TowerBuilder.Instance.build(towerId, zone.TileDict[tileId]))
                RpcCanBuild(tileId);
            else
                RpcFailBuild(tileId);
        }
    }

    [ClientRpc]
    public void RpcCanBuild(int id)
    {
        Debug.Log("can build");
    }
    [ClientRpc]
    public void RpcFailBuild(int id)
    {
        Debug.Log("You can't build on tile " + id);
    }
}
