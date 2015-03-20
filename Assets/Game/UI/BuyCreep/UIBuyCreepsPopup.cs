using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UIBuyCreepsPopup : UIElement
{
    [SerializeField]
    private CreepSpawner creepSpawner;
    [SerializeField]
    private Transform contentPanel;

    private BarracksUpgrade barackUpgrade;
    public BarracksUpgrade BarackUpgrade
    {
        set { barackUpgrade = value; }
    }

    [SerializeField]
    private GameObject creepInfoPrefab;

    [SerializeField]
    private RectTransform scrollViewPanelRect;
    
    private List<CreepInfo> buttons;
    private Catalog catalog;

    void Awake()
    {
        buttons = new List<CreepInfo>();
    }

    public void upgrade(Catalog newCatalog)
    {
        catalog = newCatalog;
        clearPanel();
        for(int i = 0; i < catalog.Spawns.Count; ++i)
        {
            CreepInfo obj = ((GameObject)Instantiate(creepInfoPrefab)).GetComponent<CreepInfo>();
            CreepUIINfo ci = catalog.Spawns[i].GetComponent<CreepUIINfo>();
            CreepMoney cm = catalog.Spawns[i].GetComponent<CreepMoney>();

            obj.BuyCreepUI = this;
            obj.Id = i;
            obj.Name = ci.Name;
            obj.Price = cm.Price.ToString();
            obj.Income = cm.IncomeIncrease.ToString();
            obj.Image = ci.Icon;

            buttons.Add(obj);
        }
        StartCoroutine(showCreepInfo());
    }

    private void clearPanel()
    {
        foreach (CreepInfo ci in buttons)
            Destroy(ci.gameObject);
        buttons.Clear();
    }

    IEnumerator showCreepInfo()
    {
        yield return null;

        foreach (CreepInfo obj in buttons)
            obj.transform.SetParent(contentPanel);
    }

    public void buyCreep(int index)
    {
        creepSpawner.requestSpawn(index);
    }

    public void upgrade()
    {
        barackUpgrade.upgrade();
    }

    public void hide()
    {
        setActive(false);
    }
}
