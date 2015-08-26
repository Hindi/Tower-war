using UnityEngine;
using UnityEngine.EventSystems;
using System;
using System.Collections;
using System.Collections.Generic;

public class KeyCombinaisonCatcher : MonoBehaviour, IPointerUpHandler
{
    [SerializeField]
    private SettingsControlLine controlLine;

    private Combinaison pressedKey;
    public Combinaison PressedKey
    { 
        set 
        {
            pressedKey = value;
        } 
    }

    bool editing = false;
    bool clickedAction = false;

    void Start()
    {

    }

    public void onEditStart()
    {
        editing = true;
        controlLine.startEditing(true);
        //StartCoroutine(startEditCoroutine());
    }

    private IEnumerator startEditCoroutine()
    {
        yield return null;
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData e)
    {
        onEditStart();
        pressedKey.reset();
    }

    public void updateControlLine()
    {
        clickedAction = pressedKey.clickedAction;
        controlLine.tryAddCombinaison(pressedKey.clone());
        controlLine.startEditing(false);
        editing = false;
        pressedKey.reset();
    }

    void Update()
    {
        if (editing)
        {
            foreach (KeyCode k in Enum.GetValues(typeof(KeyCode)))
            {
                //If a key is pressed, we want to catch it
                if ((Input.GetKeyDown(k)) && pressedKey[0] != k && pressedKey[1] != k)
                {
                    if(pressedKey[0] == KeyCode.None)
                    {
                        pressedKey[0] = k;
                        Debug.Log(pressedKey[0] + " " + pressedKey[1]);
                    }
                    else
                        pressedKey[1] = k;

                    if (pressedKey[1] != KeyCode.None)
                    {
                        Debug.Log(pressedKey[0] + " " + pressedKey[1]);
                        updateControlLine();
                        return;
                    }
                }

                //If one of the pressed key is up, then we stop catching inputs
                if (Input.GetKeyUp(k) && pressedKey[0] == k)
                {
                    updateControlLine();
                    return;
                }
            }
        }
    }
}
