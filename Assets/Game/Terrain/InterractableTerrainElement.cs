using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.EventSystems;
using System.Collections;

public abstract class InterractableTerrainElement : NetworkBehaviour {

    public abstract void onMouseOver();
    public abstract void onMouseExit();
    public abstract void onMouseDown();
    public abstract void onMouseUp();

    void Start()
    {
    }

    protected bool hoveringUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    protected virtual bool canInterract()
    {
        return (!hoveringUI());
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
