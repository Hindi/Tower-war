using UnityEngine;
using System.Collections;

public static class CombatManager
{
    public static void solveAttack(TowerStats from, CreepStats to)
    {
        to.GetComponent<CreepMortality>().takeDamage(from.Damage);
	}
}
