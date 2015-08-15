using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class Combinaison
{
    KeyCode[] keys;

    public bool clickedAction;

    public Combinaison()
    {
        reset();
        clickedAction = false;
    }

    public Combinaison(bool clicked, params KeyCode[] k)
    {
        reset();
        keys = k;
        clickedAction = clicked;
    }

    public bool isEmpty()
    {
        return (keys[0] == KeyCode.None && keys[1] == KeyCode.None);
    }

    public bool equals(Combinaison k)
    {
        return ((keys[0] == k[0] && keys[1] == k[1]) || (keys[0] == k[1] && keys[1] == k[0]) );
    }

    public string ToString()
    {
        string s = "";
        if (keys[0] != KeyCode.None) 
            s += keys[0];
        if (keys[1] != KeyCode.None) 
            s += " + " + keys[1];
        return s;
    }

    public KeyCode this[int i]
    {
        get { return keys[i]; }
        set { keys[i] = value; }
    }

    public int Length
    {
        get { return keys.Length; }
    }

    public void reset()
    {
        keys = new KeyCode[2];
        keys[0] = KeyCode.None;
        if(!clickedAction)
            keys[1] = KeyCode.None;
    }

    public Combinaison clone()
    {
        return new Combinaison(clickedAction, keys[0], keys[1]);
    }
}

public class SettingsControls : SettingsAbstract
{
    [SerializeField]
    private ControlsManager controlsManager;
    [SerializeField]
    private GameObject controlLinePrefab;
    [SerializeField]
    private Transform scrollContainer;
    [SerializeField]
    private Text notificationText;
    [SerializeField]
    private UIConfirm uiConfirm;

    private Dictionary<InputAction, Combinaison> inputs;
    private List<SettingsControlLine> lines;

    void Awake()
    {
        inputs = new Dictionary<InputAction, Combinaison>();
        lines = new List<SettingsControlLine>();
    }

    public void tryAddCombinaison(Combinaison keys, InputAction action, Action<Combinaison> callback)
    {
        bool hasDouble = false;
        foreach (KeyValuePair<InputAction, Combinaison> p in inputs)
        {
            if(p.Value.equals(keys) && p.Key != action)
            {
                SettingsControlLine duplicatedScl = null;
                foreach (SettingsControlLine scl in lines)
                    if (scl.Action == p.Key)
                        duplicatedScl = scl;

                Action doValidate = delegate()
                {
                    duplicatedScl.newKeyCombinaison(new Combinaison());
                    inputs[p.Key] = new Combinaison();
                    inputs[action] = keys;
                    callback(keys);
                };

                uiConfirm.askConfirm(TextDB.Instance().getText("confirmAlreadyUsedKeys", p.Key.ToString()), doValidate);
                hasDouble = true;
                return;
            }
        }

        if(!hasDouble)
        {
            inputs[action] = keys;
            callback(keys);
        }
    }

    public override void resetToCurrent()
    {
        foreach (Transform t in scrollContainer)
            Destroy(t.gameObject);
        lines.Clear();
    }

    public override void loadUI()
    {
        foreach (KeyValuePair<InputAction, Combinaison> p in inputs)
        {
            GameObject obj = (GameObject)Instantiate(controlLinePrefab);
            SettingsControlLine scl = obj.GetComponent<SettingsControlLine>();
            obj.GetComponent<KeyCombinaisonCatcher>().PressedKey = p.Value;
            scl.Title = p.Key.ToString();
            scl.Action = p.Key;
            scl.KeyTextPlaceHolder = p.Value.ToString();
            scl.SettingControl = this;
            obj.transform.SetParent(scrollContainer);
            lines.Add(scl);
        }
    }

    public override void loadFromSave()
    {
        PlayerPrefs.DeleteAll();
        if (PlayerPrefs.HasKey("controls"))
        {
            foreach (InputAction ia in Enum.GetValues(typeof(InputAction)))
            {
                if (PlayerPrefs.HasKey(ia.ToString() + "_0") && PlayerPrefs.HasKey(ia.ToString() + "_1"))
                {
                    bool clickedAction = PlayerPrefs.GetInt(ia.ToString() + "_clicked") == 0;
                    inputs.Add(ia, new Combinaison(clickedAction, (KeyCode)PlayerPrefs.GetInt(ia.ToString() + "_0"), (KeyCode)PlayerPrefs.GetInt(ia.ToString() + "_1")));
                }
            }
            validateSettings();
        }
        else
            resetSettings();
    }

    public override bool anythingChanged()
    {
        foreach (SettingsControlLine scl in lines)
            if (scl.hasChanged())
                return true;
        return false;
    }

    public override void validateSettings()
    {
        PlayerPrefs.SetInt("controls", 0);
        foreach (KeyValuePair<InputAction, Combinaison> p in inputs)
        {
            controlsManager.addOrReplaceChecker(p.Key, p.Value);
            PlayerPrefs.SetInt(p.Key.ToString() + "_clicked", (p.Value.clickedAction ? 0 : 1));
            PlayerPrefs.SetInt(p.Key.ToString() + "_0", (int)p.Value[0]);
            PlayerPrefs.SetInt(p.Key.ToString() + "_1", (int)p.Value[1]);
        }
    }

    public override void resetSettings()
    {
        inputs.Clear();
        inputs.Add(InputAction.sell, new Combinaison(false, KeyCode.S, KeyCode.None));
        inputs.Add(InputAction.upgrade, new Combinaison(false, KeyCode.U, KeyCode.None));
        inputs.Add(InputAction.escape, new Combinaison(false, KeyCode.Escape, KeyCode.None));
        inputs.Add(InputAction.focusOnTarget, new Combinaison(false, KeyCode.Space, KeyCode.None));
        inputs.Add(InputAction.multipleSelection, new Combinaison(true, KeyCode.LeftControl, KeyCode.Mouse0));
        inputs.Add(InputAction.build, new Combinaison(true, KeyCode.LeftShift, KeyCode.Mouse0));

        inputs.Add(InputAction.selectionGroup1, new Combinaison(false, KeyCode.LeftShift, KeyCode.Alpha1));
        inputs.Add(InputAction.selectionGroup2, new Combinaison(false, KeyCode.LeftShift, KeyCode.Alpha2)); 
        inputs.Add(InputAction.selectionGroup3, new Combinaison(false, KeyCode.LeftShift, KeyCode.Alpha3));
        inputs.Add(InputAction.selectionGroup4, new Combinaison(false, KeyCode.LeftShift, KeyCode.Alpha4));
        inputs.Add(InputAction.selectionGroup5, new Combinaison(false, KeyCode.LeftShift, KeyCode.Alpha5));
        inputs.Add(InputAction.selectionGroup6, new Combinaison(false, KeyCode.LeftShift, KeyCode.Alpha6));
        inputs.Add(InputAction.selectionGroup7, new Combinaison(false, KeyCode.LeftShift, KeyCode.Alpha7));

        inputs.Add(InputAction.selectTower1, new Combinaison(false, KeyCode.F1, KeyCode.None));
        inputs.Add(InputAction.selectTower2, new Combinaison(false, KeyCode.F2, KeyCode.None));
        inputs.Add(InputAction.selectTower3, new Combinaison(false, KeyCode.F3, KeyCode.None));
        inputs.Add(InputAction.selectTower4, new Combinaison(false, KeyCode.F4, KeyCode.None));
        inputs.Add(InputAction.selectTower5, new Combinaison(false, KeyCode.F5, KeyCode.None));
        inputs.Add(InputAction.selectTower6, new Combinaison(false, KeyCode.F6, KeyCode.None));
        inputs.Add(InputAction.selectTower7, new Combinaison(false, KeyCode.F7, KeyCode.None));
        validateSettings();
    }

    public void notifyEditing(bool b)
    {
        if(b)
        {
            notificationText.text = "Start editing";
        }
        else
        {
            notificationText.text = "";
        }
    }
}