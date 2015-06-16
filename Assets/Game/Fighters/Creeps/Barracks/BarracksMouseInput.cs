using UnityEngine;
using System.Collections;

public class BarracksMouseInput : InterractableTerrainElement
{
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
        if (canInterract())
        {
            UI.Instance.showBuyCreepsPopup();
        }
    }

    public override void onMouseUp()
    {
        if (canInterract())
        {

        }
    }
}
