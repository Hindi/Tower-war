using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;

public class UIHealthBar : NetworkBehaviour {

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

    private GameObject creep;

    private Vector3 offset;
    
    public void setHealthPercentage(float percentage)
    {
        if (isServer && !isClient)
            RpcSetHealthPercentage(percentage);

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

        if (isServer)
            RpcReset();
    }

    public void init(GameObject obj)
    {
        initialize(obj);
        RpcInit(obj);
        reset();
    }

    [ClientRpc]
    public void RpcReset()
    {
        reset();
    }

    [ClientRpc]
    public void RpcInit(GameObject obj)
    {
        initialize(obj);
        reset();
    }

    [ClientRpc]
    public void RpcSetHealthPercentage(float percentage)
    {
        setHealthPercentage(percentage);
    }

    private void initialize(GameObject obj)
    {
        creep = obj;
        originalWidth = bar.rectTransform.sizeDelta.x;
        offset = new Vector3(0, 0.5f, 0);
        transform.SetParent(obj.transform.parent);
    }

    void Update()
    {
        transform.position = creep.transform.position + offset;
    }
}
