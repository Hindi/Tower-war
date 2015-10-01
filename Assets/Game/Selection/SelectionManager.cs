using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class SelectionManager : MonoBehaviour 
{
    class SelectionGroup
    {
        public List<GameObject> selections;

        public SelectionGroup()
        {
            selections = new List<GameObject>();
        }

        public void tryAddElement(GameObject obj)
        {
            if (selections.Contains(obj))
                selections.Remove(obj);
            else
                selections.Add(obj);
        }
    }

    [SerializeField]
    private UIConfirm uiconfirm;

    [SerializeField]
    private Player player;

    [SerializeField]
    private SettingsControls settingsControls;

    private CameraMovement cameraMovement;

    private List<GameObject> selections;
    private SelectionGroup[] selectionGroups;

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

    private const int SELECTION_GROUP_COUNT = 7;

    void Start()
    {
        cameraMovement = Camera.main.GetComponent<CameraMovement>();
        selections = new List<GameObject>();
        ControlsManager.Instance.addKeyListener(InputAction.sell, onSell);
        ControlsManager.Instance.addKeyListener(InputAction.upgrade, onUpgrade);
        ControlsManager.Instance.addKeyListener(InputAction.clearSelection, onClear);
        ControlsManager.Instance.addKeyListener(InputAction.focusOnTarget, onFocusOnTarget);

        selectionGroups = new SelectionGroup[SELECTION_GROUP_COUNT];
        for(int i = 0; i < SELECTION_GROUP_COUNT; ++i)
        {
            selectionGroups[i] = new SelectionGroup();
            int id = i;
            ControlsManager.Instance.addKeyListener(InputAction.selectGroup1 + i, delegate()
            {
                onSelectGroup(id);
            });
            ControlsManager.Instance.addKeyListener(InputAction.addSelectionGroup1 + i, delegate()
            {
                onAddToSelectGroup(id);
            });
        }
    }

    void OnDestroy()
    {
        ControlsManager.Instance.removeKeyListener(InputAction.sell, onSell);
        ControlsManager.Instance.removeKeyListener(InputAction.upgrade, onUpgrade);
        ControlsManager.Instance.removeKeyListener(InputAction.clearSelection, onClear);
        ControlsManager.Instance.removeKeyListener(InputAction.focusOnTarget, onFocusOnTarget);
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

    public void onFocusOnTarget()
    {
        if (selections.Count == 0)
            return;

        if(cameraMovement != null)
            cameraMovement.goToPosition(selections[0].transform.position);
    }

    public void onSelectGroup(int i)
    {
        Combinaison addToGroupKey = settingsControls.getInputCombinaison(InputAction.addSelectionGroup1);

        //Don't select the group if the player wanted to add a tower in this group (add : shift + 0, select : 0)
        if (Input.GetKey(addToGroupKey[0]))
            return;

        //Don't clear the list if the selection group is empty
        if (selectionGroups[i].selections.Count == 0)
            return;

        clearLlist();

        for (int j = 0; j < selectionGroups[i].selections.Count; ++j)
        {
            GameObject obj = selectionGroups[i].selections[j];
            OccupentHolder oh = obj.GetComponent<OccupentHolder>();
            if(oh != null)
            {
                if (oh.IsOccupied)
                {
                    selections.Add(obj);
                    obj.GetComponent<SpriteSwitcher>().setSelected();
                }
                else
                    selectionGroups[i].selections.Remove(obj);
            }

        }
    }

    public void onAddToSelectGroup(int i)
    {
        selections.ForEach(s =>
        {
            selectionGroups[i].tryAddElement(s);
        });
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
