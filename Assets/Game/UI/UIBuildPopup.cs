using UnityEngine;
using System.Collections;

public class UIBuildPopup : UIElement {

    [SerializeField]
    private TowerBuilder towerBuilder;

    [SerializeField]
    private GameObject menu;

    private Tile currentTile;

    public void popUp(Tile tile)
    {
        setActive(true);
        Vector2 pos = Camera.main.WorldToScreenPoint(tile.transform.position);
        menu.transform.position = new Vector3(pos.x, pos.y);
        currentTile = tile;
    }

    public void build(int i)
    {
        EnumSpawn spawn;
        switch(i)
        {
            case 0:
                spawn = EnumSpawn.TOWER;
                break;
            case 1:
                spawn = EnumSpawn.TOWER2;
                break;
            default:
                spawn = EnumSpawn.TOWER;
                break;
        }

        if(currentTile != null && towerBuilder.canBuild())
            towerBuilder.build(spawn, currentTile);

        hide();
    }

    public void hide()
    {
        currentTile.GetComponent<SpriteSwitcher>().setIdleSprite();
        setActive(false);
    }
}
