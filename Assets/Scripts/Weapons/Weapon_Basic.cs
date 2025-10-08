using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Basic : Weapon
{
    protected override IEnumerator FireRoutine()
    {
        Projectile projectile = Instantiate(config.projectile, transform.position, transform.rotation).GetComponent<Projectile>();
        projectile.Launch(config, _targetsInRange[0]);
        yield break;
    }
}
