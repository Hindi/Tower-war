using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class LifeCount : NetworkBehaviour {

    private Counter counter;

    [SerializeField]
    private UILife uiLife;

    [SyncVar]
    private int count;

    [SyncVar]
    private int enemyCount;

    private LifeCount enemyLifeCount;

	// Use this for initialization
    void Start()
    {
        counter = GetComponent<Counter>();
        counter.EndCountingCallback = lost;
        EventManager.AddListener(EnumEvent.REACHEDBASE, reachedBaseListener);
        EventManager.AddListener(EnumEvent.START, onGameStart);
	}

    void Update()
    {
        if(isClient)
        {
            uiLife.updateMyLifeLabel(count);
            uiLife.updateEnemyLifeLabel(enemyCount);
        }
    }

    public void reachedBaseListener()
    {
        counter.decreaseAmount();
        updateUIs();
    }

    public void initEnemyLifeCount(LifeCount lifeCount)
    {
        enemyLifeCount = lifeCount;
    }

    public void updateEnemyLifecount(int count)
    {
        enemyCount = count;
    }

    private void updateUIs()
    {
        count = counter.CurrentAmount;
        if (enemyLifeCount)
            enemyLifeCount.updateEnemyLifecount(count);
    }

    void onGameStart()
    {
        updateUIs();
    }

    public void lost()
    {
        EventManager.Raise(EnumEvent.GAMELOST);
    }
}
