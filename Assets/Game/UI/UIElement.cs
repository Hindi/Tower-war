using UnityEngine;
using System.Collections;

public class UIElement : MonoBehaviour {

    [SerializeField]
    protected RectTransform canvasRect;
    [SerializeField]
    protected GameObject menu;
    [SerializeField]
    protected RectTransform uiPanelRect;

    public void setActive(bool b)
    {
        canvasRect.gameObject.SetActive(b);
    }

    public virtual void popUp(Vector3 position)
    {
        setActive(true);
        Vector2 pos = RectTransformUtility.WorldToScreenPoint(Camera.main,  position);
        uiPanelRect.anchoredPosition = pos - canvasRect.sizeDelta / 2f; ;
    }
}
