using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Player : NetworkBehaviour
{
    [SerializeField]
    private Zone zone;
    [SerializeField]
    private TowerBuilder towerBuilder;

    [SerializeField]
    private GameObject UI;

    void Start()
    {
        if (!isLocalPlayer)
            UI.SetActive(false);
    }

    public bool isMine()
    {
        return isLocalPlayer;
    }

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

    [Command]
    public void CmdRequestUpgrade(int tileId)
    {
        Debug.Log("Request upgrade on " + tileId);
        Tile tile = zone.TileDict[tileId];
        towerBuilder.upgrade(tile, tile.GetComponent<OccupentHolder>().occupent.GetComponent<TowerUpgrade>());
    }

    [ClientRpc]
    public void RpcCanBuild(int id)
    {
        Debug.Log("[FEEDBACK] You built a tower on position " + id);
    }
    [ClientRpc]
    public void RpcFailBuild(int id)
    {
        Debug.Log("[FEEDBACK]You can't build on tile " + id + " for reasons");
    }
}
