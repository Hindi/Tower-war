using UnityEngine;
using System.Collections;

public class TowerBuilder : MonoBehaviour {

    [SerializeField]
    private GameObject tower;

    public GameObject buildTower(GameObject tile)
    {
        GameObject newTower =  (GameObject)Instantiate(tower, tile.transform.position, Quaternion.identity);
        newTower.transform.parent = transform;
        return newTower;
    }

    public void destroyTower(GameObject tower)
    {
        Destroy(tower);
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
