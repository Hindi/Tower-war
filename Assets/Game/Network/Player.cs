using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Player : NetworkBehaviour
{
    [SerializeField]
    private Zone zone;

    [Command]
    public void CmdCanBuild(int id)
    {
        Debug.Log("can build");
        if (zone.canBuildHere(id))
            RpcBuild();
    }

    [ClientRpc]
    public void RpcBuild()
    {
        Debug.Log("can build");
    }
}
