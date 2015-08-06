using UnityEngine;
using UnityEngine.EventSystems;
using System;
using System.Collections;
using System.Collections.Generic;

public class KeyCombinaisonCatcher : MonoBehaviour, IPointerUpHandler
{
    [SerializeField]
    private SettingsControlLine controlLine;

    private List<KeyCode> pressedKey;
    bool editing = false;

    public void onEditStart()
    {
        editing = true;
        //StartCoroutine(startEditCoroutine());
    }

    private IEnumerator startEditCoroutine()
    {
        yield return null;
    }

    void Start()
    {
        pressedKey = new List<KeyCode>();
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData e)
    {
        onEditStart();
    }

    public void updateControlLine()
    {
        Debug.Log(pressedKey[0]);
        editing = false;
        pressedKey.Clear();
    }

    void Update()
    {
        if (editing)
        {

            if (Input.anyKey)
            {
                foreach (KeyCode k in Enum.GetValues(typeof(KeyCode)))
                {
                    //If a key is pressed, we want to catch it
                    if ((Input.GetKeyDown(k)) && !pressedKey.Contains(k))
                    {
                        pressedKey.Add(k);
                        if (pressedKey.Count > 1)
                        {
                            updateControlLine();
                            return;
                        }
                    }

                    //If one of the pressed key is up, then we stop catching inputs
                    if(Input.GetKeyUp(k) && pressedKey.Contains(k))
                    {
                        updateControlLine();
                        return;
                    }
                }
            }
        }
    }
}
