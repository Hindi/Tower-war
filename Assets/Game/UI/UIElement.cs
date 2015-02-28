using UnityEngine;
using System.Collections;

public class UIElement : MonoBehaviour {

    [SerializeField]
    protected Canvas canvas;
    [SerializeField]
    protected GameObject menu;

    protected void setActive(bool b)
    {
        canvas.gameObject.SetActive(b);
    }

    public void popUp(Vector3 position)
    {
        setActive(true);
        Vector2 pos = Camera.main.WorldToScreenPoint(position);
        menu.transform.position = new Vector3(pos.x, pos.y);
    }
}
