using UnityEngine;
using System.Collections;

public class InputChecker 
{
    private bool wasPressed;
    private bool repeatable;
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
        repeatable = false;
        wasPressed = false;
    }

	// Use this for initialization
    public void init(InputAction actionName, ControlsManager mgr, Combinaison keys)
    {
        keyList = keys;
        action = actionName;
        controlsManager = mgr;
	}

    private void triggerAction(bool b)
    {
        controlsManager.notifyTriggeredAction(action, b);
        wasPressed = b;
    }
	
	// Update is called once per frame
	public void update () 
    {
        bool pressed = isKeylistPressed();
        if (wasPressed && !pressed)
            triggerAction(false);

        if (pressed && !wasPressed)
            triggerAction(true);
        else if (pressed && repeatable)
            triggerAction(true);
	}

    private bool isKeylistPressed()
    {
        for(int i = 0; i < keyList.Length; ++i)
        {
            if(keyList[i] != KeyCode.None && !Input.GetKey(keyList[i]))
                return false;
        }
        return true;
    }
}
