using UnityEngine;
using System.Collections;

public class UIBuildPopup : UIElement {

    [SerializeField]
    private TowerBuilder towerBuilder;

    private Tile currentTile;

    public void popUp(Tile tile)
    {
        popUp(tile.transform.position);
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
        if (currentTile != null)
            currentTile.GetComponent<SpriteSwitcher>().setIdleSprite();
        setActive(false);
    }
}
