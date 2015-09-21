using UnityEngine;
using System.Collections;

public class CreepStats : MonoBehaviour 
{
    private CreepMovement creepMovement;
    private CreepMortality creepMortality;

    [SerializeField]
    private float speed;
    [SerializeField]
    private int maxHp;
    [SerializeField]
    private int price;
    public int Price
    {
        get { return price; }
        set { price = value; }
    }
    [SerializeField]
    private int incomeIncrease;
    public int IncomeIncrease
    {
        get { return incomeIncrease; }
        set { incomeIncrease = value; }
    }

    [SerializeField]
    private int moneyDrop;
    public int MoneyDrop
    {
        get { return moneyDrop; }
        set { moneyDrop = value; }
    }


	// Use this for initialization
	void Awake () {
        creepMovement = GetComponent<CreepMovement>();
        creepMortality = GetComponent<CreepMortality>();

        creepMovement.Speed = speed;
        creepMortality.MaxHp = maxHp;
	}

}
