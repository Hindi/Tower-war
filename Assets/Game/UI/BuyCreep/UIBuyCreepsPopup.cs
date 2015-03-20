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

    private Vector2 scrollViewOutPosition;
    private Vector2 scrollViewInPosition;
    private List<CreepInfo> buttons;
    private Catalog catalog;
    private bool poppingOut;
    private bool poppedOut;

    void Awake()
    {
        buttons = new List<CreepInfo>();
    }

    void Start()
    {
        scrollViewInPosition = scrollViewPanelRect.position;
        scrollViewOutPosition = scrollViewPanelRect.position;
        scrollViewOutPosition.x += scrollViewPanelRect.sizeDelta.x;
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

    public override void popUp(Vector3 position)
    {
        popUp();
    }

    public void fromUiPopup(bool popIn)
    {
        if (!popIn && !poppedOut)
            return;
        popUp();
    }

    public void popUp()
    {
        if (poppingOut)
        {
            poppingOut = !poppingOut;
            StartCoroutine(slideInCoroutine());
        }
        else
        {
            poppingOut = !poppingOut;
            StartCoroutine(slideOutCoroutine());
        }
    }

    IEnumerator slideInCoroutine()
    {
        while (!poppingOut && Vector2.Distance(scrollViewPanelRect.position, scrollViewInPosition) > 0.1f)
        {
            scrollViewPanelRect.position = Vector3.Lerp(scrollViewPanelRect.position, scrollViewInPosition, 10 * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator slideOutCoroutine()
    {
        while (poppingOut)
        {
            scrollViewPanelRect.position = Vector3.Lerp(scrollViewPanelRect.position, scrollViewOutPosition, 10 * Time.deltaTime);
            if (Vector2.Distance(scrollViewPanelRect.position, scrollViewOutPosition) < 0.1f)
            {
                poppedOut = true;
                break;
            }
            yield return null;
        }
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
