using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public enum InputAction
{
    sell,
    upgrade,
    escape,
    focusOnTarget,
    scrollUp,
    scrollDown,
    build,
    multipleSelection,
    clearSelection,
    selectionGroup1,
    selectionGroup2,
    selectionGroup3,
    selectionGroup4,
    selectionGroup5,
    selectionGroup6,
    selectionGroup7,
    selectTower1,
    selectTower2,
    selectTower3,
    selectTower4,
    selectTower5,
    selectTower6,
    selectTower7
}

public class ControlsManager : MonoBehaviour 
{
    public delegate void InputCallback();
    Dictionary<InputAction, Delegate> registeredActions;
    Dictionary<InputAction, bool> actionStates;
    List<InputChecker> checkers;
    private bool blockInputs;

    private static ControlsManager instance;
    public static ControlsManager Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType<ControlsManager>();
            return instance;
        }
    }

    void Awake()
    {
        checkers = new List<InputChecker>();
        registeredActions = new Dictionary<InputAction, Delegate>();
        actionStates = new Dictionary<InputAction, bool>();
        
        foreach (InputAction ia in Enum.GetValues(typeof(InputAction)))
            actionStates.Add(ia, false);
    }

	// Use this for initialization
	void Start () 
    {
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

    public void notifyTriggeredAction(InputAction actionName, bool b)
    {
        actionStates[actionName] = b;
        if(b)
        {
            Delegate d;
            if (registeredActions.TryGetValue(actionName, out d))
            {
                InputCallback callback = (InputCallback)d;
                if (callback != null)
                    callback();
            }
        }
    }

    public bool isActionTriggered(InputAction inputAction)
    {
        return actionStates[inputAction];
    }

    public void addKeyListener(InputAction inputAction, InputCallback action)
    {
        if(!registeredActions.ContainsKey(inputAction))
            registeredActions.Add(inputAction, null);
        registeredActions[inputAction] = (InputCallback)registeredActions[inputAction] + action;
    }

    public void removeKeyListener(InputAction inputAction, InputCallback action)
    {
        if (registeredActions.ContainsKey(inputAction))
        {
            registeredActions[inputAction] = (InputCallback)registeredActions[inputAction] - action;

            if (registeredActions[inputAction] == null)
            {
                registeredActions.Remove(inputAction);
            }
        }
    }
}
