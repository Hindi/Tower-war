using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum InputAction
{
    selectTower1,
    selectTower2,
    selectTower3,
    selectTower4,
    selectTower5,
    selectTower6,
    selectTower7,
    selectionGroup1,
    selectionGroup2,
    selectionGroup3,
    selectionGroup4,
    selectionGroup5,
    selectionGroup6,
    selectionGroup7,
    sell,
    upgrade,
    escape,
    focusOnTarget,
    scrollUp,
    scrollDown,
    multipleSelection
}

public class ControlsManager : MonoBehaviour 
{
    List<InputChecker> checkers;
    private bool blockInputs;

	// Use this for initialization
	void Start () 
    {
        checkers = new List<InputChecker>();
        EventManager<bool>.AddListener(EnumEvent.BLOCKINPUTS, onBlockInputs);
	}

    void OnDestroy()
    {
        EventManager<bool>.RemoveListener(EnumEvent.BLOCKINPUTS, onBlockInputs);
    }
	
	// Update is called once per frame
	void Update () {
        if(!blockInputs)
            checkers.ForEach(c => c.update());
	}

    public void onBlockInputs(bool b)
    {
        blockInputs = b;
    }

    public void addOrReplaceChecker(InputAction action, Combinaison inputs)
    {
        foreach(InputChecker checker in checkers)
        {
            if(checker.Action == action)
            {
                checker.KeyList = inputs;
                return;
            }
        }
        checkers.Add(new InputChecker(action, this, inputs));
    }

    public void notifyTriggeredAction(InputAction actionName)
    {
        Debug.Log(actionName);
    }
}
