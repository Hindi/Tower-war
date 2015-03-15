using UnityEngine;
using System.Collections;

public class BarracksBuy : MonoBehaviour {


    [SerializeField]
    private Factory factory;

    [SerializeField]
    private CreepSpawner creepSpawner;

    [SerializeField]
    private Income income;

    public void requestBuy(int i)
    {
        creepSpawner.requestSpawn(i);
    }
}
