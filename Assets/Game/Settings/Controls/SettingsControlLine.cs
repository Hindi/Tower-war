using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class SettingsControlLine : MonoBehaviour
{
    [SerializeField]
    private Text title;
    public string Title
    { set { title.text = value; } }

    [SerializeField]
    private Text keyText;
    public string KeyText
    { set { keyText.text = value; } }

    [SerializeField]
    private Text keyTextPlaceHolder;
    public string KeyTextPlaceHolder
    { set { keyTextPlaceHolder.text = value; } }

    public bool hasChanged()
    {
        return keyTextPlaceHolder != keyText;
    }

    
}
