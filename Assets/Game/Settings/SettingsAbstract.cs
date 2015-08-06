using UnityEngine;
using System.Collections;

public abstract class SettingsAbstract : MonoBehaviour 
{
    public abstract void resetToCurrent();
    public abstract void loadUI();
    public abstract void loadFromSave();
    public abstract bool anythingChanged();
    public abstract void validateSettings();
    public abstract void resetSettings();
}
