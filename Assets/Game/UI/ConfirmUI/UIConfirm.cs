using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class UIConfirm : MonoBehaviour 
{
    [SerializeField]
    private GameObject canvas;
    [SerializeField]
    private Text cancelText;
    [SerializeField]
    private Text confirmText;
    [SerializeField]
    private Text questionText;

    private Action confirmCallback;

    private void Awake()
    {
        EventManager.AddListener(EnumEvent.LOADTEXTS, onLoadText);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener(EnumEvent.LOADTEXTS, onLoadText);
    }

    public void onLoadText()
    {
        cancelText.text = TextDB.Instance().getText("cancel");
        confirmText.text = TextDB.Instance().getText("confirm");
    }

    public void confirm()
    {
        if (confirmCallback != null)
            confirmCallback();
        canvas.SetActive(false);
    }

    public void cancel()
    {
        confirmCallback = null;
        canvas.SetActive(false);
    }

    public void askConfirm(string text, Action action)
    {
        questionText.text = text;
        confirmCallback = action;
        canvas.SetActive(true);
    }
}
