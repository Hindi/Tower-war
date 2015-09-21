using UnityEngine;
using System.Collections;

public class TowerStats : MonoBehaviour
{
    private TowerMoney money;
    private TowerFocus focus;
    private TowerCannon cannon;
    private TowerHead head;

    [SerializeField]
    private int price;
    public int Price
    { get { return price; } }

    [SerializeField]
    private int upgradePrice;
    public int UpgradePrice
    { get { return upgradePrice; } }

    [SerializeField]
    private int radius;
    public int Radius
    { get { return radius; } }

    [SerializeField]
    private int damage;
    public int Damage
    { get { return damage; } }

    [SerializeField]
    private int fireCooldown;
    public int FireCooldown
    { get { return fireCooldown; } }

    [SerializeField]
    private float speed;
    public float Speed
    { get { return speed; } }

    void Awake()
    {
        cannon = GetComponent<TowerCannon>();
        focus = GetComponent<TowerFocus>();
        money = GetComponent<TowerMoney>();
        head = GetComponent<TowerHead>();

        money.Price = price;
        money.UpgradePrice = upgradePrice;
        focus.Radius = radius;
        cannon.FireCooldown = fireCooldown;
        head.Speed = speed;
    }
}
