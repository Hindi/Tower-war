using UnityEngine;
using System.Collections;

public class TileCreepTrigger : MonoBehaviour {

    [SerializeField]
    private OccupentHolder occupentHolder;


    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Creep"))
        {
            occupentHolder.notifyCreepLeave();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Creep"))
        {
            occupentHolder.notifyCreepEnter();
        }
    }
}
