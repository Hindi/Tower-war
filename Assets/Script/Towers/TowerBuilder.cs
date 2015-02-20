using UnityEngine;
using System.Collections;

public class TowerBuilder : MonoBehaviour {

    [SerializeField]
    private GameObject tower;

    public GameObject buildTower(Vector3 position)
    {
        return (GameObject)Instantiate(tower, position, Quaternion.identity);
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
