using UnityEngine;
using System.Collections;

public static class DamageCalculator 
{
    public static int processDamage(TowerStats attaquer, CreepStats target)
    {
        return attaquer.Damage;
    }
}
