using UnityEngine;
using System.Collections;

public class Settings : MonoBehaviour
{
    private int resX; 
    private int resY; 
    private int isFullScreen;

    void Awake()
    {
        loadVideoSettings();
    }

    private void loadVideoSettings()
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
            isFullScreen = PlayerPrefs.GetInt("fullscreen");
        }
        else
        {
            resetFullScreen();
        }
        Screen.SetResolution(resX, resY, (isFullScreen == 0));
    }

    public void setResolution(int width, int height)
    {
        PlayerPrefs.SetInt("resolutionX", width);
        PlayerPrefs.SetInt("resolutionY", height);
        resX = width;
        resY = height;
        Screen.SetResolution(resX, resY, (isFullScreen == 0));
    }

    public void setFullScreen(bool fs)
    {
        isFullScreen = System.Convert.ToInt32(fs);
        Screen.fullScreen = fs;
    }

    private void resetToDefaultResolution()
    {
        resX = Screen.currentResolution.width;
        resY = Screen.currentResolution.height;
        PlayerPrefs.SetInt("resolutionX", Screen.currentResolution.width);
        PlayerPrefs.SetInt("resolutionY", Screen.currentResolution.height);
        Screen.SetResolution(resX, resY, (isFullScreen == 0));
    }

    private void resetFullScreen()
    {
        isFullScreen = 0;
        PlayerPrefs.SetInt("fullscreen", System.Convert.ToInt32(isFullScreen));
        Screen.fullScreen = (isFullScreen == 0);
    }

    public void resetVideoSettings()
    {
        resetFullScreen();
        resetToDefaultResolution();
    }
}
