using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class SelectionManager : MonoBehaviour 
{
    [SerializeField]
    private UIConfirm uiconfirm;

    [SerializeField]
    private Player player;

    private List<GameObject> selections;

    private static SelectionManager instance;
    public static SelectionManager Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType<SelectionManager>();
            return instance;
        }
    }

    void Start()
    {
        selections = new List<GameObject>();
        ControlsManager.Instance.addKeyListener(InputAction.sell, onSell);
        ControlsManager.Instance.addKeyListener(InputAction.upgrade, onUpgrade);
        ControlsManager.Instance.addKeyListener(InputAction.clearSelection, onClear);
    }

    void OnDestroy()
    {
        ControlsManager.Instance.removeKeyListener(InputAction.sell, onSell);
        ControlsManager.Instance.removeKeyListener(InputAction.upgrade, onUpgrade);
        ControlsManager.Instance.removeKeyListener(InputAction.clearSelection, onClear);
    }

    private void clearLlist()
    {
        foreach(GameObject obj in selections)
        {
            TileMouseInput tmi = obj.GetComponent<TileMouseInput>();
            if(tmi)
                tmi.resetToIdle();
        }
        selections.Clear();
    }

    public void onSell()
    {
        if (selections.Count == 0)
            return;

        string text = (selections.Count > 1 ? TextDB.Instance().getText("sellSeveralTower") : TextDB.Instance().getText("sellOneTower"));
        uiconfirm.askConfirm(text, delegate
        {
            foreach (GameObject obj in selections)
            {
                TileMouseInput tmi = obj.GetComponent<TileMouseInput>();
                Tile tile = obj.GetComponent<Tile>();
                if (tmi && tile)
                {
                    tmi.resetToIdle();
                    player.CmdRequestSell(tile.Id);
                }
            }
            selections.Clear();
        });
    }

    public void onUpgrade()
    {
        if (selections.Count == 0)
            return;

        foreach (GameObject obj in selections)
        {
            Tile tile = obj.GetComponent<Tile>();
            player.CmdRequestUpgrade(tile.Id);
        }
    }

    public void onClear()
    {
        if (selections.Count == 0)
            return;

        clearLlist();
    }

    public void selectNew(GameObject obj)
    {
        clearLlist();
        selectAnother(obj);
    }

    public void selectAnother(GameObject obj)
    {
        selections.Add(obj);
    }
}
