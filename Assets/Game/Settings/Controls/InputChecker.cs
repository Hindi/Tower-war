using UnityEngine;
using System.Collections;

public class InputChecker 
{
    private Combinaison keyList;
    public Combinaison KeyList
    { set { keyList = value; } }

    private InputAction action;
    public InputAction Action
    { get { return action; } }

    private ControlsManager controlsManager;

    public InputChecker(InputAction actionName, ControlsManager mgr, Combinaison keys)
    {
        init(actionName, mgr, keys);
    }

	// Use this for initialization
    public void init(InputAction actionName, ControlsManager mgr, Combinaison keys)
    {
        keyList = keys;
        action = actionName;
        controlsManager = mgr;
	}
	
	// Update is called once per frame
	public void update () 
    {
	    if(isKeylistPressed())
        {
            controlsManager.notifyTriggeredAction(action);
        }
	}

    private bool isKeylistPressed()
    {
        for(int i = 0; i < keyList.Length; ++i)
        {
            if(!Input.GetKey(keyList[i]))
                return false;
        }
        return true;
    }
}
