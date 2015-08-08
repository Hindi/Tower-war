using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class SettingsControlLine : MonoBehaviour
{
    [SerializeField]
    private GameObject inactiveObj;
    [SerializeField]
    private GameObject activeObj;

    [SerializeField]
    private Text title;
    public string Title
    { set { title.text = value; } }

    [SerializeField]
    private Text keyTextPlaceHolder;
    public string KeyTextPlaceHolder
    { set { keyTextPlaceHolder.text = value; } }

    private SettingsControls settingControl;
    public SettingsControls SettingControl
    { set { settingControl = value; } }

    private string prevKey;

    private InputAction action;
    public InputAction Action
    { 
        set { action = value; }
        get { return action; }
    }

    public bool hasChanged()
    {
        return keyTextPlaceHolder.text != prevKey;
    }

    public void tryAddCombinaison(Combinaison keys)
    {
        settingControl.tryAddCombinaison(keys, action, newKeyCombinaison);
    }

    public void newKeyCombinaison(Combinaison keys)
    {
        prevKey = keyTextPlaceHolder.text;
        keyTextPlaceHolder.text = keys.ToString();
    }

    public void startEditing(bool b)
    {
        EventManager<bool>.Raise(EnumEvent.BLOCKINPUTS, b);
        settingControl.notifyEditing(b);
        activeObj.SetActive(b);
        inactiveObj.SetActive(!b);
    }
}
