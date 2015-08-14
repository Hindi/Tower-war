using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SettingsToggle : MonoBehaviour
{
    [SerializeField]
    private Toggle toggle;

    [SerializeField]
    private string settingName;
    public string SettingName
    { get { return settingName; } }

    private bool value;
    public bool Value
    { get { return value; } }

    private bool selectedValue;

    public bool hasChanged()
    {
        return (value != selectedValue);
    }

    public void applyModification()
    {
        value = selectedValue;
    }

    public void onOptionClick()
    {
        selectedValue = toggle.isOn;
    }

    public void resetToCurrent()
    {
        selectedValue = value;
        toggle.isOn = value;
    }

    public void setValue(bool b)
    {
        value = b;
        selectedValue = b;
        toggle.isOn = b;
    }
}
