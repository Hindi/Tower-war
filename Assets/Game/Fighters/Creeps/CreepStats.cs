using UnityEngine;
using System.Collections;

public class CreepStats : MonoBehaviour 
{
    private CreepMovement creepMovement;
    private CreepMortality creepMortality;
    private CreepMoney creepMoney;

    [SerializeField]
    private float speed;
    [SerializeField]
    private int maxHp;
    [SerializeField]
    private int price;
    [SerializeField]
    private int incomeIncrease;
    [SerializeField]
    private int moneyDrop;

	// Use this for initialization
	void Awake () {
        creepMovement = GetComponent<CreepMovement>();
        creepMortality = GetComponent<CreepMortality>();
        creepMoney = GetComponent<CreepMoney>();

        creepMovement.Speed = speed;
        creepMortality.MaxHp = maxHp;
        creepMoney.Price = price;
        creepMoney.IncomeIncrease = incomeIncrease;
        creepMoney.MoneyDrop = moneyDrop;
	}

}
