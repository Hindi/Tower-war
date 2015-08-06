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
    [SerializeField]
    private Toggle fullscreenToggle;

    private int resX; 
    private int resY;
    private bool isFullScreen;

    private bool isFullScreenSelection;
    private int resXSelection;
    private int resYSelection;

    private void Start()
    {
        foreach (SettingsAbstract s in settingsList)
        {
            s.loadFromSave();
            s.loadUI();
        }
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

    public void resetSettings()
    {
        settingsList.ForEach(s => s.resetSettings());
    }

    public void validateSettings()
    {
        foreach (SettingsAbstract s in settingsList)
        {
            if (s.anythingChanged())
            {
                uiConfirm.askConfirm(TextDB.Instance().getText("confirmApplySettings"), validateSettings, withdrawSettings);
                return;
            }
        }
    }

    public void withdrawSettings()
    {
        resetToCurrent();
    }
}
