using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DropDownElement : MonoBehaviour 
{
    [SerializeField]
    private Text buttonText;

    private DropDown dropDown;
    public DropDown Dropdown
    { set { dropDown = value; } }

    public void setText(string text)
    {
        buttonText.text = text;
    }

    public void onClick()
    {
        dropDown.notifySelection(buttonText.text);
    }
}
