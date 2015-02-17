using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Zone : MonoBehaviour {

    [SerializeField]
    private GameObject hexagonPrefab;

    [SerializeField]
    private float timeBeforeFadeAway;
    private float mouseLeaveTime;

    private Dictionary<int, Hexagon> hexagonDict;

	// Use this for initialization
	void Start () {
        hexagonDict = new Dictionary<int, Hexagon>();

        //Calculate the dimension that we'll use
        float width = 0;
        float height = 0;
        float offestW;
        float offsetL = 0;

        GameObject currentHexa;
        Hexagon currHexaScript;

        for (int j = 0; j < 20; ++j)
        {
            for (int i = 0; i < 10; ++i)
            {
                currentHexa = (GameObject)Instantiate(hexagonPrefab);
                currHexaScript = currentHexa.GetComponent<Hexagon>();
                Rect rect = currentHexa.GetComponent<SpriteSwitcher>().CurrentSprite.rect;

                width = rect.width / 100;
                height = rect.height / 84;
                offestW = width / 2;
                currentHexa.transform.position = new Vector3(offsetL + i * (width + offestW), j * height / 2.4f, 0);
                currHexaScript.Zone = this;
                currHexaScript.calcId();

                hexagonDict.Add(currHexaScript.Id, currHexaScript);
            }
            if (offsetL == 0)
                offsetL = width * 2 / 2.69f;
            else
                offsetL = 0;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void notifyMouseOver()
    {
        foreach (Transform g in transform)
        {
            g.GetComponent<Fader>().setRendererVisible(true);
        }
    }

    public void notifyMouseExit()
    {
        foreach(Transform g in transform)
        {
            g.GetComponent<Fader>().setRendererVisible(false);
        }
    }
}
