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

    private NetworkConnection connection;

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
        if (zone.canBuildHere(tileId))
        {
            towerBuilder.build(towerId, zone.TileDict[tileId]);
        }
    }

    [Command]
    public void CmdRequestUpgrade(int tileId)
    {
        Tile tile = zone.TileDict[tileId];
        towerBuilder.upgrade(tile, tile.GetComponent<OccupentHolder>().occupent.GetComponent<TowerUpgrade>());
    }

    [Command]
    public void CmdRequestSell(int tileId)
    {
        Tile tile = zone.TileDict[tileId];
        if (tile)
        {
            towerBuilder.sell(tile);
        }
    }
}
