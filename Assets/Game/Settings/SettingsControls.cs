using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SettingsControls : SettingsAbstract
{
    [SerializeField]
    private ControlsManager controlsManager;
    [SerializeField]
    private GameObject controlLinePrefab;
    [SerializeField]
    private Transform scrollContainer;

    private Dictionary<InputAction, KeyCode[]> inputs;
    private List<SettingsControlLine> lines;

    void Start()
    {
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
        foreach (KeyValuePair<InputAction, KeyCode[]> p in inputs)
        {
            GameObject obj = (GameObject)Instantiate(controlLinePrefab);
            SettingsControlLine scl = obj.GetComponent<SettingsControlLine>();
            scl.Title = p.Key.ToString();
            scl.KeyTextPlaceHolder = "ta mere";
            obj.transform.SetParent(scrollContainer);
            lines.Add(scl);
        }
    }

    public override void loadFromSave()
    {
        inputs = new Dictionary<InputAction, KeyCode[]>();
        lines = new List<SettingsControlLine>();
        inputs.Add(InputAction.test, new KeyCode[] { KeyCode.Space, KeyCode.A });
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
        foreach(KeyValuePair<InputAction, KeyCode[]> p in inputs)
        {
            controlsManager.addOrReplaceChecker(p.Key, p.Value);
        }
    }

    public override void resetVideoSettings()
    {

    }

}