using UnityEngine;
using UnityEngine.EventSystems;
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
        return EventSystem.current.IsPointerOverGameObject();
    }

    protected virtual bool canInterract()
    {
        return (!hoveringUI() && photonView.isMine);
    }

    void OnMouseOver()
    {
        onMouseOver();
    }

    void OnMouseExit()
    {
        onMouseExit();
    }

    void OnMouseDown()
    {
        onMouseDown();
    }

    void OnMouseUp()
    {
        onMouseUp();
    }
}
