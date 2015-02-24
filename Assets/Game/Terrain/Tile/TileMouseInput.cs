using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Tile))]
public class TileMouseInput : InterractableTerrainElement
{

    [SerializeField]
    private OccupentHolder occupentHolder;

    public Rect spriteRect()
    {
        return GetComponent<SpriteSwitcher>().CurrentSprite.rect;
    }

    private bool hoveringUI()
    {
        return UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
    }

    public void setVisible(bool b)
    {
        renderer.enabled = b;
    }

    public override void onMouseOver()
    {
        if (!hoveringUI())
        {
            GetComponent<SpriteSwitcher>().setMouseOverSprite();
            GetComponent<Tile>().Zone.notifyMouseOver();
        }
    }

    public override void onMouseExit()
    {
        if (!hoveringUI())
        {
            resetToIdle();
            GetComponent<Tile>().Zone.notifyMouseExit();
        }
    }

    public override void onMouseDown()
    {
        if (!hoveringUI())
        {
            GetComponent<SpriteSwitcher>().setSelected();
            //TODO : Use UI to build and destroy the tower
            if (occupentHolder.IsOccupied)
            {
                occupentHolder.destroyOccupent();
            }
            else if (occupentHolder.canBuild())
            {
                UI.Instance.showBuildPopup(GetComponent<Tile>());
            }
        }
    }

    public override void onMouseUp()
    {
        if (!hoveringUI())
        {
            resetToIdle();
        }
    }

    void resetToIdle()
    {
        GetComponent<SpriteSwitcher>().setPreviousSprite();
    }

}
