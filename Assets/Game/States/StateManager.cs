using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * This class manage the states in the whole game (menus and minigame)
 * 
 */

public enum EnumState
{
    GAME,
    NONE
}

public class StateManager : MonoBehaviour 
{
    private State currentState;

    [SerializeField]
    private Dictionary<EnumState, State> stateList;

    void Awake()
    {
        stateList = new Dictionary<EnumState, State>();
        currentState = new GameState(this);

        stateList.Add(EnumState.GAME, new GameState(this));
    }

	// Use this for initialization
	void Start () {
        currentState.start();
	}
	
	// Update is called once per frame
	void Update () {
        currentState.update();
	}

	
	public void OnLevelWasLoaded(int lvl)
    {
		currentState.onLevelWasLoaded (lvl);
	}

    public void noticeInput(EnumInput key)
    {
        currentState.noticeInput(key);
    }

    public void noticeInput(EnumInput key, Touch[] inputs)
    {
        currentState.noticeInput(key, inputs);
    }

    public void changeState(EnumState state)
    {
        if(state != EnumState.NONE)
        {
            currentState.end();
            currentState = stateList[state];
            currentState.start();
        }
    }
}
