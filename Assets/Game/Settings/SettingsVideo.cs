using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SettingsVideo : SettingsAbstract
{
    [SerializeField]
    private GameObject resOptionPrefab;
    [SerializeField]
    private DropDown resOptionContainer;
    [SerializeField]
    private Toggle fullscreenToggle;

    private int resX;
    private int resY;
    private bool isFullScreen;

    private bool isFullScreenSelection;
    private int resXSelection;
    private int resYSelection;

    public override void resetToCurrent()
    {
        fullscreenToggle.isOn = Screen.fullScreen;
        resXSelection = Screen.width;
        resYSelection = Screen.height;
    }

    public override void loadFromSave()
    {
        if (PlayerPrefs.HasKey("resolutionX") && PlayerPrefs.HasKey("resolutionY"))
        {
            resX = PlayerPrefs.GetInt("resolutionX");
            resY = PlayerPrefs.GetInt("resolutionY");
        }
        else
        {
            resetToDefaultResolution();
        }

        if (PlayerPrefs.HasKey("fullscreen"))
        {
            isFullScreen = PlayerPrefs.GetInt("fullscreen") == 0;
        }
        else
        {
            resetDefaultFullScreen();
        }
        Screen.SetResolution(resX, resY, isFullScreen);
    }

    public void onResolutionSelected(string s)
    {
        string[] param = s.Split('x');
        int w = System.Convert.ToInt32(param[0]);
        int h = System.Convert.ToInt32(param[1]);
        selectNewResolution(w, h);
    }

    public override void loadUI()
    {
        //Resolution option
        foreach (Resolution res in Screen.resolutions)
        {
            resOptionContainer.addItems(res.width + "x" + res.height);
        }
        resOptionContainer.selectionCallback += onResolutionSelected;
        resOptionContainer.ButtonText = Screen.width + "x" + Screen.height;

        //fullscreen option
        fullscreenToggle.isOn = Screen.fullScreen;
    }

    private void setResolution(int width, int height)
    {
        PlayerPrefs.SetInt("resolutionX", width);
        PlayerPrefs.SetInt("resolutionY", height);
        resX = width;
        resY = height;
        Screen.SetResolution(resX, resY, isFullScreenSelection);
    }

    private void setFullScreen()
    {
        isFullScreen = isFullScreenSelection;
        Screen.fullScreen = isFullScreenSelection;
        PlayerPrefs.SetInt("fullscreen", System.Convert.ToInt32(isFullScreen));
    }

    public void selectNewResolution(int width, int height)
    {
        resXSelection = width;
        resYSelection = height;
    }

    private void resetToDefaultResolution()
    {
        resX = Screen.currentResolution.width;
        resY = Screen.currentResolution.height;
        PlayerPrefs.SetInt("resolutionX", Screen.currentResolution.width);
        PlayerPrefs.SetInt("resolutionY", Screen.currentResolution.height);
        Screen.SetResolution(resX, resY, isFullScreen);
    }

    private void resetDefaultFullScreen()
    {
        isFullScreen = false;
        PlayerPrefs.SetInt("fullscreen", System.Convert.ToInt32(1));
        Screen.fullScreen = isFullScreen;
    }

    public void selectFullScreen(bool fs)
    {
        isFullScreenSelection = fullscreenToggle.isOn;
    }

    public override bool anythingChanged()
    {
#if UNITY_EDITOR
        return false;
#else
        return (resX != resXSelection || resY != resYSelection || isFullScreen != isFullScreenSelection);
#endif
    }

    public override void resetSettings()
    {
        resetDefaultFullScreen();
        resetToDefaultResolution();
    }

    public override void validateSettings()
    {
        setResolution(resXSelection, resYSelection);
        setFullScreen();
    }
}
