using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Income : NetworkBehaviour {

    private int currentIncome;
    [SerializeField]
    private int startIncome;
    [SerializeField]
    private float incomeCooldown;

    [SerializeField]
    private UIIncome incomeCanvas;

    private Purse purse;

	// Use this for initialization
	void Start () {
        purse = GetComponent<Purse>();
        currentIncome = startIncome;
        incomeCanvas.IncomeTime = incomeCooldown.ToString();
        incomeCanvas.IncomeAmount = currentIncome.ToString();
	}

    public void startCounting()
    {
        StartCoroutine(incomeCoroutine());
    }
	
    IEnumerator incomeCoroutine()
    {
        int secondCounter;
        while(true)
        {
            secondCounter = 0;
            while (secondCounter < incomeCooldown)
            {
                secondCounter++;
                RpcUpdateValues(incomeCooldown - secondCounter, currentIncome);
                yield return new WaitForSeconds(1);
            }
            purse.add(currentIncome);
        }
    }

    [ClientRpc]
    private void RpcUpdateValues(float time, int income)
    {
        incomeCanvas.IncomeTime = time.ToString();
        incomeCanvas.IncomeAmount = income.ToString();
    }

    public void increaseIncome(int amount)
    {
        currentIncome += amount;
    }
}
