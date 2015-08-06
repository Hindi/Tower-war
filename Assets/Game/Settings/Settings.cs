using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Settings : MonoBehaviour
{
    [SerializeField]
    private List<SettingsAbstract> settingsList;
    [SerializeField]
    private GameObject settingsPanel;
    [SerializeField]
    private UIConfirm uiConfirm;

    private void Start()
    {
        foreach (SettingsAbstract s in settingsList)
        {
            s.loadFromSave();
            s.loadUI();
        }
    }

    private void OnEnable()
    {

    }

    public void resetToCurrent()
    {
        settingsList.ForEach(s => s.resetToCurrent());
    }

    public void toggleDisplayPanel()
    {
        settingsPanel.SetActive(!settingsPanel.activeSelf);
    }

    public void askSettingsValidation()
    {
        foreach(SettingsAbstract s in settingsList)
        {
            if (s.anythingChanged())
            {
                uiConfirm.askConfirm(TextDB.Instance().getText("confirmApplySettings"), validateSettings, withdrawSettings);
                return;
            }
        }
    }

    public void resetVideoSettings()
    {
        settingsList.ForEach(s => s.resetVideoSettings());
    }

    public void validateSettings()
    {
        settingsList.ForEach(s => s.validateSettings());
    }

    public void withdrawSettings()
    {
        resetToCurrent();
    }
    
}
