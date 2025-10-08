using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Melee : Weapon
{
    protected override IEnumerator FireRoutine()
    {
        foreach (var target in _targetsInRange)
        {
            target.Damage(config.baseDamage);
        }
        yield break;
    }
}
