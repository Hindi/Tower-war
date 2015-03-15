using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIHealthBar : MonoBehaviour {

    [SerializeField]
    private Image bar;
    [SerializeField]
    private Image mask;

    [SerializeField]
    private Color fullHealthColor;

    [SerializeField]
    private Color midHealthColor;

    [SerializeField]
    private Color lowHealthColor;

    private float originalWidth;
    private float currentPercentage;

    [SerializeField]
    private GameObject creep;

    private Vector3 offset;

    public void Start()
    {
        originalWidth = bar.rectTransform.sizeDelta.x;
        offset = new Vector3(0, 0.5f, 0);
        reset();
    }

    public void setHealthPercentage(float percentage)
    {
        currentPercentage = percentage;
        mask.rectTransform.sizeDelta = new Vector2(originalWidth * percentage, bar.rectTransform.sizeDelta.y);
        if (percentage < 0.7f)
            bar.color = Color.yellow;
        else if (percentage < 0.4f)
            bar.color = Color.red;
    }

    public void reset()
    {
        mask.rectTransform.sizeDelta = new Vector2(originalWidth, bar.rectTransform.sizeDelta.y);
        bar.color = Color.green;
    }

    public void init(GameObject obj)
    {
        creep = obj;
    }

    void Update()
    {
        transform.position = creep.transform.position + offset;
    }
}
