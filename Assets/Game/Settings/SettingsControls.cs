using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class Combinaison
{
    KeyCode[] keys;

    public Combinaison()
    {
        reset();
    }

    public Combinaison(params KeyCode[] k)
    {
        reset();
        keys = k;
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
        if (keys[0] != KeyCode.None) s += keys[0];
        if (keys[1] != KeyCode.None) s += " + " + keys[1];
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
        keys[1] = KeyCode.None;
    }

    public Combinaison clone()
    {
        return new Combinaison(keys[0], keys[1]);
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

    private Dictionary<InputAction, Combinaison> inputs;
    private List<SettingsControlLine> lines;

    void Start()
    {
    }

    public void tryAddCombinaison(Combinaison keys, InputAction action, Func<Combinaison, int> callback)
    {
        bool hasdouble = false;
        if (keys.Length > 0)
        {
            foreach (KeyValuePair<InputAction, Combinaison> p in inputs)
            {
                if(p.Value.equals(keys))
                {
                    hasdouble = true;
                    break;
                }
            }
        }

        if(!hasdouble)
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
        loadUI();
    }

    public override void loadUI()
    {
        foreach (KeyValuePair<InputAction, Combinaison> p in inputs)
        {
            GameObject obj = (GameObject)Instantiate(controlLinePrefab);
            SettingsControlLine scl = obj.GetComponent<SettingsControlLine>();
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
        inputs = new Dictionary<InputAction, Combinaison>();
        lines = new List<SettingsControlLine>();
        inputs.Add(InputAction.test, new Combinaison(KeyCode.Space, KeyCode.A));
        inputs.Add(InputAction.test2, new Combinaison(KeyCode.Space, KeyCode.Alpha0));
        validateSettings();
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
        foreach (KeyValuePair<InputAction, Combinaison> p in inputs)
        {
            controlsManager.addOrReplaceChecker(p.Key, p.Value);
        }
    }

    public override void resetSettings()
    {

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