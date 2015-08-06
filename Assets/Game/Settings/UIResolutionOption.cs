using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIResolutionOption : MonoBehaviour 
{
    [SerializeField]
    private Text buttonText;

    private SettingsVideo settings;
    public SettingsVideo Settings
    { set { settings = value; } }

    int w;
    int h;

    public void setResolution()
    {
        settings.selectNewResolution(w, h);
    }

    public void init(int width, int height)
    {
        w = width;
        h = height;
        buttonText.text = width + "x" + height;
    }
}
