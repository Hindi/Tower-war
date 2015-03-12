using UnityEngine;
using System.Collections;

public class TowerUpgrade : MonoBehaviour {

    [SerializeField]
    private BuySpawn nextVersion = BuySpawn.DEFAULT; 

    private TowerActivity activity;

    void Start()
    {
        activity = GetComponent<TowerActivity>();
    }

    public bool asAnUpgrade()
    {
        return (nextVersion != BuySpawn.NOTHING);
    }

    public GameObject upgradeNow()
    {
        Vector3 pos = transform.position;
        activity.Active = false;
        return Factory.Instance.spawn(nextVersion, pos);
    }

}
