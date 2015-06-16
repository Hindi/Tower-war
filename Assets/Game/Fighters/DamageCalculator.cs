using UnityEngine;
using System.Collections;

public static class DamageCalculator 
{
    public static int calcDamage(CombatStats attaquer, CombatStats target)
    {
        return attaquer.damage;
    }
}
