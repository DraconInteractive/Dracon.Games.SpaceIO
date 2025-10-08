using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Projectile_Ricochet : Projectile
{
    public float checkRadius = 1f;
    public int maxRicochetCount = 3;

    private List<Enemy> hitEnemies = new List<Enemy>();
    
    public override void Launch(WeaponConfig config, Character target = null)
    {
        base.Launch(config, target);
        if (target != null)
        {
            Vector3 direction = target.transform.position - transform.position;
            transform.rotation = Quaternion.LookRotation(direction.normalized);
        }
    }

    private void FixedUpdate()
    {
        if (_killing) return;

        transform.position += transform.forward * (_config.speed * Time.deltaTime);
        CheckForHit(checkRadius);
    }

    public override void HitEnemy(Enemy hitEnemy)
    {
        hitEnemy.Damage(_config.baseDamage);
        hitEnemies.Add(hitEnemy);
        // Find next enemy excluding hit
        if (hitEnemies.Count < maxRicochetCount)
        {
            Vector3 pos = transform.position;
            var availableEnemies = 
                EnemyManager.Enemies.Where(
                    x => !hitEnemies.Contains(x) &&
                         x.alive &&
                         Vector3.Distance(x.transform.position, pos) < _config.range)
                    .ToList();
            
            int count = availableEnemies.Count();
            if (count > 0)
            {
                var rand = Random.Range(0, count);
                _target = availableEnemies[rand];
                Vector3 direction = _target.transform.position - transform.position;
                transform.rotation = Quaternion.LookRotation(direction.normalized);
            }
            else
            {
                Kill();
            }
        }
        else
        {
            Kill();
        }
    }

    public override void Kill()
    {
        _killing = true;
        Destroy(this.gameObject, GetComponentInChildren<TrailRenderer>().time);
    }
}
