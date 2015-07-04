using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class MoveTowardAndHide : NetworkBehaviour {
    [SerializeField]
    private float precision;

    [SerializeField]
    private float speed;

    [SerializeField]
    private GameObject model;

    void Start()
    {
        model.SetActive(false);
    }

    public void move(Vector3 start, Vector3 goal)
    {
        StartCoroutine(moveTowardAndHide(start, goal));
        RpcMoveTowardAndHide(start, goal);
    }

    [ClientRpc]
    public void RpcMoveTowardAndHide(Vector3 start, Vector3 goal)
    {
        StartCoroutine(moveTowardAndHide(start, goal));
    }

    IEnumerator moveTowardAndHide(Vector3 start, Vector3 goal)
    {
        transform.position = start;
        model.SetActive(true);
        while (Vector3.Distance(goal, transform.position) > precision)
        {
            transform.position = Vector3.Slerp(transform.position, goal, speed * Time.deltaTime);
            yield return null;
        }
        model.SetActive(false);
    }
}
