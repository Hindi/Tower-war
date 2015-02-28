using UnityEngine;
using System.Collections;

public abstract class InterractableTerrainElement : MonoBehaviour {

    public abstract void onMouseOver();
    public abstract void onMouseExit();
    public abstract void onMouseDown();
    public abstract void onMouseUp();

    [SerializeField]
    PhotonView photonView;

    void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    protected bool hoveringUI()
    {
        return UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
    }

    protected virtual bool canInterract()
    {
        return (!hoveringUI() && photonView.isMine);
    }
}
