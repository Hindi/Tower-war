using UnityEngine;
using System.Collections;

public class SlideInAndOut : MonoBehaviour {

    [SerializeField]
    private RectTransform panelRect;

    private Vector2 outPosition;
    private Vector2 inPosition;
    private bool poppingOut;
    private bool poppedOut;

	// Use this for initialization
    void Start()
    {
        inPosition = panelRect.position;
        outPosition = panelRect.position;
        outPosition.x += panelRect.sizeDelta.x;
	}

    public void fromUiPopup(bool popIn)
    {
        if (!popIn && !poppedOut)
            return;
        popUp();
    }

    public void popUp()
    {
        if (poppingOut)
        {
            poppingOut = !poppingOut;
            StartCoroutine(slideInCoroutine());
        }
        else
        {
            poppingOut = !poppingOut;
            StartCoroutine(slideOutCoroutine());
        }
    }

    IEnumerator slideInCoroutine()
    {
        while (!poppingOut && Vector2.Distance(panelRect.position, inPosition) > 0.1f)
        {
            panelRect.position = Vector3.Lerp(panelRect.position, inPosition, 10 * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator slideOutCoroutine()
    {
        while (poppingOut)
        {
            panelRect.position = Vector3.Lerp(panelRect.position, outPosition, 10 * Time.deltaTime);
            if (Vector2.Distance(panelRect.position, outPosition) < 0.1f)
            {
                poppedOut = true;
                break;
            }
            yield return null;
        }
    }
}
