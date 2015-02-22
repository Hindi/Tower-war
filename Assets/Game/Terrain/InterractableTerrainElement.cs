using UnityEngine;
using System.Collections;

public abstract class InterractableTerrainElement : MonoBehaviour {

    public abstract void onMouseOver();
    public abstract void onMouseExit();
    public abstract void onMouseDown();
    public abstract void onMouseUp();
}
