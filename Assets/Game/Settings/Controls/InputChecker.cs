using UnityEngine;
using System.Collections;

public class InputChecker 
{
    private KeyCode[] keyList;
    public KeyCode[] KeyList
    { set { keyList = value; } }

    private InputAction action;
    public InputAction Action
    { get { return action; } }

    private ControlsManager controlsManager;

    public InputChecker(InputAction actionName, ControlsManager mgr, params KeyCode[] keys)
    {
        init(actionName, mgr, keys);
    }

	// Use this for initialization
	public void init (InputAction actionName, ControlsManager mgr, params KeyCode[] keys) {
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
        foreach(KeyCode k in keyList)
        {
            if(!Input.GetKey(k))
                return false;
        }
        return true;
    }
}
