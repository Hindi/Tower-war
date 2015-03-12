using UnityEngine;
using System.Collections;

public class Income : MonoBehaviour {

    private int currentIncome;
    [SerializeField]
    private int startIncome;
    [SerializeField]
    private float incomeCooldown;

    private UIIncome incomeCanvas;

    private Purse purse;

	// Use this for initialization
	void Start () {
        incomeCanvas = UI.Instance.IncomeCanvas;
        purse = GetComponent<Purse>();
        currentIncome = startIncome;
        incomeCanvas.IncomeTime = incomeCooldown.ToString();
        incomeCanvas.IncomeAmount = currentIncome.ToString();

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
                incomeCanvas.IncomeTime = (incomeCooldown - secondCounter).ToString();
                yield return new WaitForSeconds(1);
            }
            purse.add(currentIncome);
        }
    }

    public void increaseIncome(int amount)
    {
        currentIncome += amount;
        incomeCanvas.IncomeAmount = currentIncome.ToString();
    }
}
