using UnityEngine;
using System.Collections;

public class UIBuyCreepsPopup : UIElement
{
    [SerializeField]
    private CreepSpawner creepSpawner;

    public void buyCreep(int spawn)
    {
        creepSpawner.requestSpawn((EnumSpawn)spawn);
    }

    public void hide()
    {
        setActive(false);
    }
}
