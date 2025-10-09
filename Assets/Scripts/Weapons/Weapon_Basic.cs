using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Basic : Weapon
{
    protected override IEnumerator FireRoutine()
    {
        Vector3 pos = transform.position;
        
        // Velocity is also calculated in update, so we may be using velocity from the last frame
        // Thats acceptable for now
        // Cant get acceleration since thats a player only thing and I dont want to do a check
        pos += _owner.Velocity * -1;
        
        Projectile projectile = Instantiate(config.projectile, pos, transform.rotation).GetComponent<Projectile>();
        projectile.Launch(config, _targetsInRange[0]);
        yield break;
    }
}
