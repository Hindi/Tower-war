using UnityEngine;
using System.Collections;

public class Counter : MonoBehaviour {

    [SerializeField]
    private int startAmount;

    [SerializeField]
    private bool canBeGreaterThanStart;

    private Callback endCountingCallback;
    public Callback EndCountingCallback
    {
        get { return endCountingCallback; }
        set { endCountingCallback = value; }
    }

    private int currentAmount;
    public int CurrentAmount
    {
        get { return currentAmount; }
    }

	// Use this for initialization
	void Start () {
        currentAmount = startAmount;
	}

    public void increaseAmount()
    {
        currentAmount++;
        if (currentAmount > startAmount && !canBeGreaterThanStart)
            currentAmount = startAmount;
    }

    public void decreaseAmount()
    {
        currentAmount--;
        if(currentAmount <= 0)
        {
            currentAmount = 0;
            if (endCountingCallback != null)
                endCountingCallback();
        }
    }

    public void reset()
    {
        currentAmount = startAmount;
    }
}
