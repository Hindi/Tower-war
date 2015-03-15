﻿using UnityEngine;
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

        if(photonView.isMine)
            photonView.RPC("resetRPC", PhotonTargets.Others);
    }

    public void init(GameObject obj)
    {
        initialize();
        creep = obj;
        int id = obj.GetComponent<PhotonView>().viewID;
        photonView.RPC("initRPC", PhotonTargets.Others, id);
        reset();
    }

    [RPC]
    public void resetRPC()
    {
        Debug.Log("pouet");
        reset();
    }

    [RPC]
    public void initRPC(int id)
    {
        initialize();
        creep = PhotonView.Find(id).gameObject;
        reset();
    }

    private void initialize()
    {
        currentPercentage = 1;
        originalWidth = bar.rectTransform.sizeDelta.x;
        offset = new Vector3(0, 0.5f, 0);
        photonView = GetComponent<PhotonView>();
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
            setHealthPercentage((float)stream.ReceiveNext());
        }
    }
}