using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIIncome : UIElement {

    [SerializeField]
    private Text incomeTime;
    public string IncomeTime
    {
        get { return incomeTime.text; }
        set { incomeTime.text = value; }
    }

    [SerializeField]
    private Text incomeAmount;
    public string IncomeAmount
    {
        get { return incomeAmount.text; }
        set { incomeAmount.text = value; }
    }

    [SerializeField]
    private Text moneyAmount;
    public string MoneyAmount
    {
        get { return moneyAmount.text; }
        set { moneyAmount.text = value; }
    }


}
