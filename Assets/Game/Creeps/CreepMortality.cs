using UnityEngine;
using System.Collections;

public class CreepMortality : MonoBehaviour {

    [SerializeField]
    private int maxHp;

    private int currentHp;

    void Start()
    {
        reset();
    }

    public void reset()
    {
        currentHp = maxHp;
    }

    public bool takeDamage(int dmg)
    {
        currentHp = Mathf.Max(0, currentHp - dmg);
        if (currentHp == 0)
        {
            die();
            return true;
        }
        return false;
    }

    public void kill()
    {
        takeDamage(currentHp);
    }

    private void die()
    {
        FxSpawner.Instance.spawn(0, transform.position);
        GetComponent<CreepActivity>().Active = false;
    }
}
