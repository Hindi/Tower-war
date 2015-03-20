using UnityEngine;
using System.Collections;

public class MoveTowardAndDestroy : MonoBehaviour {


    private Activity activity;

    [SerializeField]
    private float precision;

    [SerializeField]
    private float speed;

	// Use this for initialization
	void Start () {
        activity = GetComponent<Activity>();
	}

    public void move(Vector3 position)
    {
        StartCoroutine(moveTowardAndDestroy(position));
    }
	
    IEnumerator moveTowardAndDestroy(Vector3 pos)
    {
        while(Vector3.Distance(pos, transform.position) > precision)
        {
            transform.position = Vector3.Slerp(transform.position, pos, speed * Time.deltaTime);
            yield return null;
        }
        if (activity)
            activity.Active = false;
        else
            Destroy(gameObject);
    }
}
