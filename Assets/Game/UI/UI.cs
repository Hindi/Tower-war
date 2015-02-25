﻿using UnityEngine;
using System.Collections;

public class UI : MonoBehaviour {

    private static UI instance;

    public static UI Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType<UI>();
            return instance;
        }
    }

    [SerializeField]
    private UIBuildPopup buildPopup;
    [SerializeField]
    private UIUpgradePopup upgradePopup;

    public void showBuildPopup(Tile tile)
    {
        buildPopup.popUp(tile);
    }

    public void showUpgradePopupp(Tile tile)
    {
        upgradePopup.popUp(tile);
    }
}