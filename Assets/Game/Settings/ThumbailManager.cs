using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ThumbailManager : MonoBehaviour 
{
    [SerializeField]
    List<GameObject> thumbmails;

	// Use this for initialization
	void Start () {
	
	}

    void OnEnable()
    {
        hideAll();
        thumbmails[0].SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    private void hideAll()
    {
        thumbmails.ForEach(tm => tm.SetActive(false));
    }

    public void displayThumbmail(int id)
    {
        if(id >= 0 && id < thumbmails.Count)
        {
            hideAll();
            thumbmails[id].SetActive(true);
        }
    }
}
