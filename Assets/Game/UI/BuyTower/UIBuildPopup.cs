using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UIBuildPopup : UIElement 
{

    private Tile currentTile;

    private List<TowerInfo> buttons;
    private Catalog catalog;

    [SerializeField]
    private GameObject towerInfoPrefab;
    [SerializeField]
    private Transform contentPanel;
    [SerializeField]
    private Image currentTowerImage;

    void Awake()
    {
        buttons = new List<TowerInfo>();
    }

    public void popUp(Tile tile)
    {
        GetComponent<SlideInAndOut>().popUp();
        currentTile = tile;
    }

    IEnumerator showTowerInfo()
    {
        yield return null;

        foreach (TowerInfo obj in buttons)
            obj.transform.SetParent(contentPanel);
    }

    public void upgrade(Catalog newCatalog)
    {
        catalog = newCatalog;
        clearPanel();
        for (int i = 0; i < catalog.Spawns.Count; ++i)
        {
            TowerInfo obj = ((GameObject)Instantiate(towerInfoPrefab)).GetComponent<TowerInfo>();
            ProdutUIInfo ci = catalog.Spawns[i].GetComponent<ProdutUIInfo>();
            TowerMoney tm = catalog.Spawns[i].GetComponent<TowerMoney>();

            obj.BuyTowerUI = this;
            obj.Id = i;
            obj.Name = ci.Name;
            obj.Price = tm.Price.ToString();
            obj.Image = ci.Icon;

            buttons.Add(obj);
        }
        StartCoroutine(showTowerInfo());
    }

    private void clearPanel()
    {
        foreach (TowerInfo ci in buttons)
            Destroy(ci.gameObject);
        buttons.Clear();
    }

    public void build(int i)
    {
        currentTowerImage.overrideSprite = buttons[i].Image;
        TowerBuilder.Instance.SelectedTower = i;
        /*if (currentTile != null && currentTile.GetComponent<OccupentHolder>().canBuild())
            TowerBuilder.Instance.build(i, currentTile);*/
        Debug.Log(i);
    }

    public void hide()
    {
        GetComponent<SlideInAndOut>().popUp();
    }
}
