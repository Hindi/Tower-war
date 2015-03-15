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

    private GameObject creep;

    PhotonView photonView;

    private Vector3 offset;

    public void Start()
    {
        photonView = GetComponent<PhotonView>();
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
        int id = obj.GetComponent<PhotonView>().viewID;
        photonView.RPC("initRPC", PhotonTargets.Others, id);
    }

    [RPC]
    public void initRPC(int id)
    {
        creep = PhotonView.Find(id).gameObject;
    }

    void Update()
    {
        transform.position = creep.transform.position + offset;
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(currentPercentage);
        }
        else
        {
            setHealthPercentage(currentPercentage);
        }
    }
}
