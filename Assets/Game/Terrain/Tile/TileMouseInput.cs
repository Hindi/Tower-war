using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Tile))]
public class TileMouseInput : InterractableTerrainElement
{
    [SerializeField]
    private OccupentHolder occupentHolder;

    Tile tile;
    Player player;
    float lastClickedTime;

    void Start()
    {
        tile = GetComponent<Tile>();
        lastClickedTime = 0;
    }

    public void setPlayer(Player p)
    {
        player = p;
    }

    public Rect spriteRect()
    {
        return GetComponent<SpriteSwitcher>().CurrentSprite.rect;
    }

    public void setVisible(bool b)
    {
        GetComponent<Renderer>().enabled = b;
    }

    public override void onMouseOver()
    {
        if (canInterract())
        {
        }
    }

    public override void onMouseExit()
    {
        if (canInterract())
        {
        }
    }

    public override void onMouseDown()
    {
        StartCoroutine(mouseDownEndOfFrame());
    }

    private IEnumerator mouseDownEndOfFrame()
    {
        yield return new WaitForEndOfFrame();
        if (canInterract())
        {
            if (ControlsManager.Instance.isActionTriggered(InputAction.build))
            {
                player.CmdRequestBuild(tile.Id, TowerBuilder.Instance.SelectedTower);
            }
            if (occupentHolder.IsOccupied)
            {
                GetComponent<SpriteSwitcher>().setSelected();
                if (ControlsManager.Instance.isActionTriggered(InputAction.multipleSelection))
                {
                    SelectionManager.Instance.selectAnother(gameObject);
                }
                else
                {
                    if (Time.time - lastClickedTime < 0.3f)
                        SelectionManager.Instance.selectByType(gameObject);
                    else
                        SelectionManager.Instance.selectNew(gameObject);
                }

                lastClickedTime = Time.time;
            }
        }
    }

    public override void onMouseUp()
    {
        if (canInterract())
        {

        }
    }

    public void resetToIdle()
    {
        GetComponent<SpriteSwitcher>().setIdleSprite();
    }

    protected override bool canInterract()
    {
        if (player == null)
            return false;
        return base.canInterract() && player.isMine();
    }
}
