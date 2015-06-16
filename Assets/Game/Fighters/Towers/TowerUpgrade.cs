using UnityEngine;
using System.Collections;

public class TowerUpgrade : MonoBehaviour {

    [SerializeField]
    private string nextVersion = ""; 

    private TowerActivity activity;

    void Start()
    {
        activity = GetComponent<TowerActivity>();
    }

    public bool hasAnUpgrade()
    {
        return (nextVersion != "");
    }

    public GameObject upgradeNow(Vector3 pos, GOF.GOFactory towerFactory)
    {
        activity.Active = false;
        return towerFactory.spawn(nextVersion, pos);
    }

}
