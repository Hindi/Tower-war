using UnityEngine;
using System.Collections;

public class Income : MonoBehaviour {

    private int currentIncome;
    [SerializeField]
    private int startIncome;
    [SerializeField]
    private float incomeCooldown;

    private Purse purse;

	// Use this for initialization
	void Start () {
        purse = GetComponent<Purse>();
        currentIncome = startIncome;

        StartCoroutine(incomeCoroutine());
	}
	
    IEnumerator incomeCoroutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(incomeCooldown);
            purse.add(currentIncome);
        }
    }

    public void increaseIncome(int amount)
    {
        currentIncome += amount;
    }
}
