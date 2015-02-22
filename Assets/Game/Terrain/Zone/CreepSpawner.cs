using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Pathfinder))]
public class CreepSpawner : MonoBehaviour {

    private Pathfinder pathfinder;

    private GameObject startTile;
    public GameObject StartTile
    {
        get { return startTile; }
        set { startTile = value; }
    }

    private GameObject endTile;
    public GameObject EndTile
    {
        get { return endTile; }
        set { endTile = value; }
    }

    [SerializeField]
    private CreepFactory creepFactory;

	// Use this for initialization
	void Start () {
        pathfinder = GetComponent<Pathfinder>();

        EventManager.AddListener(EnumEvent.START, onGameStart);
	}

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            GameObject creep = creepFactory.spawnCreep(EnumCreep.BASIC, startTile.transform.position);
            creep.GetComponent<CreepMovement>().Path = pathfinder.Result;
            creep.GetComponent<CreepMovement>().Pathfinder = pathfinder;
        }
    }

    void onGameStart()
    {
        updatePath();
    }

    void updatePath()
    {
        pathfinder.StartId = startTile.GetComponent<Tile>().Id;
        pathfinder.GoalId = endTile.GetComponent<Tile>().Id;
        pathfinder.findPath();
    }
}
