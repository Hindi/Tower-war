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
    bool editing = false;

    void Start()
    {
        pressedKey = new Combinaison();
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
    }

    public void updateControlLine()
    {
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
                        pressedKey[0] = k;
                    else
                        pressedKey[1] = k;

                    if (pressedKey[1] != KeyCode.None)
                    {
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
