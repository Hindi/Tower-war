using UnityEngine;
using System.Collections;

public class SpriteSwitcher : MonoBehaviour {

    [SerializeField]
    private Sprite idle;

    [SerializeField]
    private Sprite path;

    [SerializeField]
    private Sprite mouseOver;

    [SerializeField]
    private Sprite mouseClic;

    [SerializeField]
    private GameObject spriteHolder;
    public GameObject SpriteHolder
    {
        get { return spriteHolder; }
    }


    [SerializeField]
    private SpriteRenderer spriteRenderer;

    private Sprite currentSprite;
    public Sprite CurrentSprite
    {
        get { return currentSprite; }
        set 
        {
            currentSprite = value;
            spriteRenderer.sprite = value;
        }
    }

    void Awake()
    {
        spriteRenderer = spriteHolder.GetComponent<SpriteRenderer>();
        currentSprite = spriteRenderer.sprite;
        //setOpacitiy(0);
    }

    public void setRendererVisible(bool b)
    {
        spriteRenderer.enabled = b;
    }

    public void setMouseOverSprite()
    {
        setActiveSprite(mouseOver);
    }

    public void setIdleSprite()
    {
        setActiveSprite(idle);
    }

    public void setPathSprite()
    {
        setActiveSprite(path);
    }

    public void setMouseClickSprite()
    {
        setActiveSprite(mouseClic);
    }

    void setActiveSprite(Sprite s)
    {
        CurrentSprite = s;
    }

    public void reduceOpacity(float value)
    {
        Color curColor = spriteRenderer.color;
        if (spriteRenderer.color.a + value > 1)
            return;

        spriteRenderer.color = new Color(curColor.r, curColor.g, curColor.b, curColor.a + value);
    }

    public void addOpacity(float value)
    {
        Color curColor = spriteRenderer.color;
        if (spriteRenderer.color.a - value < 0)
            return;
        spriteRenderer.color = new Color(curColor.r, curColor.g, curColor.b, curColor.a - value);
    }

    public void setOpacitiy(float value)
    {
        value = Mathf.Min(Mathf.Max(value, 0), 1);
        Color curColor = spriteRenderer.color;
        spriteRenderer.color = new Color(curColor.r, curColor.g, curColor.b, value);
    }

    public bool isTransparent()
    {
        return (spriteRenderer.color.a == 100);
    }
}
