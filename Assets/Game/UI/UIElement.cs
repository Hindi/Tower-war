using UnityEngine;
using System.Collections;

public class UIElement : MonoBehaviour {

    [SerializeField]
    private Canvas canvas;

    protected void setActive(bool b)
    {
        canvas.gameObject.SetActive(b);
    }
}
