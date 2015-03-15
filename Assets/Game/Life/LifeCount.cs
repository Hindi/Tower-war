using UnityEngine;
using System.Collections;

public class LifeCount : MonoBehaviour {

    private Counter counter;

	// Use this for initialization
	void Start () {
        counter = GetComponent<Counter>();
        counter.EndCountingCallback = lost;
        EventManager.AddListener(EnumEvent.REACHEDBASE, reachedBaseListener);
	}

    public void reachedBaseListener()
    {
        counter.decreaseAmount();
    }

    public void lost()
    {
        EventManager.Raise(EnumEvent.GAMELOST);
    }
}
