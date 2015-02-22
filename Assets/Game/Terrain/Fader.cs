using UnityEngine;
using System.Collections;

public class Fader : MonoBehaviour {

    [SerializeField]
    private SpriteSwitcher spriteSwitcher;

    [SerializeField]
    [Range(1,3)]
    private float fadeSpeed;

    private bool goToVisible;

    public void setRendererVisible(bool b)
    {
        goToVisible = b;
    }

    void Update()
    {
        if(goToVisible)
        {
            if (spriteSwitcher.isTransparent())
                goToVisible = false;
            else
                spriteSwitcher.reduceOpacity(fadeSpeed * Time.deltaTime);
        }
        else
        {
            spriteSwitcher.addOpacity(fadeSpeed * Time.deltaTime);
        }
    }
}
