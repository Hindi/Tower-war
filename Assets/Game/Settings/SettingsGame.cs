using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SettingsGame : SettingsAbstract
{
    [SerializeField]
    List<SettingsToggle> toggles;

    public override void resetToCurrent()
    {
        toggles.ForEach(t => t.resetToCurrent());
    }

    public override void loadFromSave()
    {

    }

    public override void loadUI()
    {

    }

    public override bool anythingChanged()
    {
        foreach (SettingsToggle t in toggles)
            if (t.hasChanged())
                return true;
        return false;
    }

    public override void resetSettings()
    {
        toggles.ForEach(t => t.resetToCurrent());
    }

    public override void validateSettings()
    {
        foreach(SettingsToggle t in toggles)
        {
            switch(t.SettingName)
            {
                case "skipConfirm":
                    Debug.Log("Validate skip confirm");
                    break;
            }
        }
    }
}
