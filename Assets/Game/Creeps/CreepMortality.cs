using UnityEngine;
using System.Collections;

public class CreepMortality : MonoBehaviour {

    [SerializeField]
    private int maxHp;

    private UIHealthBar healthBar;
    [SerializeField]
    private GameObject healthBarPrefab;

    private int currentHp;

    void Start()
    {
        healthBar = ((GameObject)Instantiate(healthBarPrefab, transform.position, Quaternion.identity)).GetComponent<UIHealthBar>();
        healthBar.init(gameObject);
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
    }
}
