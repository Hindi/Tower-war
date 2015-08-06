using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DropDown : MonoBehaviour 
{
    [SerializeField]
    private GameObject dropDownPanel;
    [SerializeField]
    private GameObject elementPrefab;
    [SerializeField]
    private GameObject elementContainer;
    [SerializeField]
    private Text buttonText;
    public string ButtonText
    { set { buttonText.text = value; } }

    public delegate void onSelectionCallback(string s);
    public onSelectionCallback selectionCallback;

	void Start () {
        dropDownPanel.SetActive(false);
	}

    public void addItems(params string[] items)
    {
        foreach(string s in items)
        {
            GameObject obj = (GameObject)Instantiate(elementPrefab);
            DropDownElement dde = obj.GetComponent<DropDownElement>();
            dde.setText(s);
            dde.Dropdown = this;
            dde.transform.SetParent(elementContainer.transform);
        }
    }

    public void toggleShowPanel()
    {
        dropDownPanel.SetActive(!dropDownPanel.activeSelf);
    }

    public void hidePanel()
    {
        dropDownPanel.SetActive(false);
    }

    public void notifySelection(string s)
    {
        if(selectionCallback != null)
        {
            selectionCallback(s);
        }
        hidePanel();
        buttonText.text = s;
    }
}
