using UnityEngine;
using System.Collections;

public class Activity : MonoBehaviour
{
    PhotonView photonView;

    [SerializeField]
    private float inactivityTimeBeforeDestroy;

    [SerializeField]
    protected GameObject model;

    protected bool isActive;
    public bool Active
    {
        get { return isActive; }
        set
        {
            isActive = value;
            activate(isActive);
            if (!isActive)
                StartCoroutine(destroyCoroutine());
        }
    }

    protected Machine machine;
    public Machine Machine
    {
        set { machine = value; }
    }

    protected virtual void activate(bool b)
    {
        hide(b);
    }

    void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    IEnumerator destroyCoroutine()
    {
        float coroutineStartTime = Time.time;
        while (!isActive)
        {
            if (Time.time - coroutineStartTime > inactivityTimeBeforeDestroy)
            {
                machine.remove(gameObject);
                break;
            }
            yield return new WaitForSeconds(30);
        }
    }

    protected void hide(bool b)
    {
        model.SetActive(b);
        if(!b)
            transform.position = new Vector3(0, 1000, 0);

        if(photonView.isMine)
        {
            photonView.RPC("newPosition", PhotonTargets.Others, transform.position);
            photonView.RPC("hideRPC", PhotonTargets.Others, b);
        }
    }

    [RPC]
    public void hideRPC(bool b)
    {
        hide(b);
    }

    [RPC]
    public void newPosition(Vector3 pos)
    {
        transform.position = pos;
    }
}
