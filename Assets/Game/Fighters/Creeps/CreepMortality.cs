using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class CreepMortality : NetworkBehaviour {

    [SerializeField]
    private int maxHp;

    [SerializeField]
    private UIHealthBar healthBar;

    [SerializeField]
    private GameObject healthBarPrefab;

    private int currentHp;

    void Start()
    {
        if (isServer)
        {
            healthBar = ((GameObject)Instantiate(healthBarPrefab, transform.position, Quaternion.identity)).GetComponent<UIHealthBar>();
            NetworkServer.Spawn(healthBar.gameObject);
            healthBar.init(gameObject);
        }
        reset();
    }

    public void reset()
    {
        currentHp = maxHp;
        healthBar.reset();
    }

    public bool takeDamage(int dmg)
    {
        currentHp = Mathf.Max(0, currentHp - dmg);
        healthBar.setHealthPercentage((float)currentHp / (float)maxHp);
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
        Destroy(healthBar.gameObject);
    }
}
