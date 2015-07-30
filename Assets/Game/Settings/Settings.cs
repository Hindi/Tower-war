using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Settings : MonoBehaviour
{

    [SerializeField]
    private GameObject resOptionPrefab;
    [SerializeField]
    private Transform resOptionContainer;
    [SerializeField]
    private UIConfirm uiConfirm;

    private int resX; 
    private int resY; 
    private int isFullScreen;

    private bool anythingChanged;
    private bool isFullScreenSelection;
    private int resXSelection;
    private int resYSelection;

    void Awake()
    {
        //loadVideoSettings();
    }

    void Start()
    {
        loadResolutionUI();
    }

    void OnEnable()
    {
        anythingChanged = false;
        resXSelection = Screen.width;
        resYSelection = Screen.height;
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

    private void loadResolutionUI()
    {
        foreach(Resolution res in Screen.resolutions)
        {
            GameObject obj = (GameObject)Instantiate(resOptionPrefab);
            obj.GetComponent<UIResolutionOption>().Settings = this;
            obj.GetComponent<UIResolutionOption>().init(res.width, res.height);
            obj.transform.SetParent(resOptionContainer);
        }
    }

    public void setResolution(int width, int height)
    {
        PlayerPrefs.SetInt("resolutionX", width);
        PlayerPrefs.SetInt("resolutionY", height);
        resX = width;
        resY = height;
        Screen.SetResolution(resX, resY, Screen.fullScreen);
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

    public void toggleFullScreen()
    {
        setFullScreen(!Screen.fullScreen);
    }

    public void askSettingsValidation()
    {
        if (anythingChanged)
            uiConfirm.askConfirm(TextDB.Instance().getText("confirmApplySettings"), validateSettings);
    }

    public void validateSettings()
    {
        setFullScreen(isFullScreenSelection);
        setResolution(resXSelection, resYSelection);
    }
    
}
