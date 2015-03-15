using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UILife : MonoBehaviour 
{
    [SerializeField]
    private Text myLifeLabel;
    [SerializeField]
    private Text enemyLifeLabel;

    public void updateMyLifeLabel(int value)
    {
        myLifeLabel.text = value.ToString();
    }

    public void updateEnemyLifeLabel(int value)
    {
        enemyLifeLabel.text = value.ToString();
    }
}
