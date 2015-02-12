using UnityEngine;
using System.Collections;

public abstract class InterractableTerrainElement : MonoBehaviour {

    protected abstract void onMouseOver();
    void OnMouseOver()
    {
        onMouseOver();
    }


    protected abstract void onMouseExit();
    void OnMouseExit()
    {
        onMouseExit();
    }
}
