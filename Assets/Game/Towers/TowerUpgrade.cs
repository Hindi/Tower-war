using UnityEngine;
using System.Collections;

public class TowerUpgrade : MonoBehaviour {

    [SerializeField]
    private int nextVersion = -1; 

    private TowerActivity activity;

    void Start()
    {
        activity = GetComponent<TowerActivity>();
    }

    public bool hasAnUpgrade()
    {
        return (nextVersion != -1);
    }

    public GameObject upgradeNow(Vector3 pos, Factory towerFactory)
    {
        activity.Active = false;
        return towerFactory.spawn(nextVersion, pos);
    }

}
