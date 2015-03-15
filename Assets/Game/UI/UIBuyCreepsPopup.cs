using UnityEngine;
using System.Collections;

public class UIBuyCreepsPopup : UIElement
{
    [SerializeField]
    private CreepSpawner creepSpawner;

    public void buyCreep(int index)
    {
        creepSpawner.requestSpawn(index);
    }

    public void hide()
    {
        setActive(false);
    }
}
