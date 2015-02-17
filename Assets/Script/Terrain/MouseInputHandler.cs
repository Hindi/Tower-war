using UnityEngine;
using System.Collections;

public class MouseInputHandler : MonoBehaviour {

    [SerializeField]
    private InterractableTerrainElement interractableElement;

    void OnMouseOver()
    {
        interractableElement.onMouseOver();
    }

    void OnMouseExit()
    {
        interractableElement.onMouseExit();
    }

    void OnMouseDown()
    {
        interractableElement.onMouseDown();
    }

    void OnMouseUp()
    {
        interractableElement.onMouseUp();
    }
}
