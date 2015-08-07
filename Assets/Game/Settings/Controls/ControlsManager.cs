using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum InputAction
{
    test,
    test2
}

public class ControlsManager : MonoBehaviour 
{
    List<InputChecker> checkers;

	// Use this for initialization
	void Start () 
    {
        checkers = new List<InputChecker>();
	}
	
	// Update is called once per frame
	void Update () {
        checkers.ForEach(c => c.update());
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
