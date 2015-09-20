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

    public void init(CreepSpawner spawner)
    {
        creepSpawner = spawner;
    }

    public void upgrade(Catalog newCatalog)
    {
        catalog = newCatalog;
        clearPanel();
        for(int i = 0; i < catalog.Spawns.Count; ++i)
        {
            CreepInfo obj = ((GameObject)Instantiate(creepInfoPrefab)).GetComponent<CreepInfo>();
            ProdutUIInfo ci = catalog.Spawns[i].GetComponent<ProdutUIInfo>();
            CreepStats cm = catalog.Spawns[i].GetComponent<CreepStats>();

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
        Debug.LogWarning("UPGRADE CREEP NOT IMPLEMENTED");
    }

    public void hide()
    {
        setActive(false);
    }
}
