using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Projectile_Piercing : Projectile
{
    public float checkRadius;
    private List<Enemy> _hitEnemies = new List<Enemy>();
    
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
        CheckForHits(checkRadius);
    }

    protected override void CheckForHits(float checkRadius)
    {
        if (_targetingPlayer)
        {
            if (Vector3.Distance(transform.position, _target.transform.position) < checkRadius)
            {
                _target.Damage(_config.baseDamage);
                Kill();
            }
        }
        else
        {
            var hitEnemies = EnemyManager.GetAllInRange(transform.position, checkRadius);
            if (hitEnemies != null && hitEnemies.Count > 0)
            {
                foreach (var enemy in hitEnemies.Where(x => !_hitEnemies.Contains(x)))
                {
                    HitEnemy(enemy);
                }
            }
        }
    }

    public override void HitEnemy(Enemy hitEnemy)
    {
        _hitEnemies.Add(hitEnemy);
        hitEnemy.Damage(_config.baseDamage);
    }

    public override void Kill()
    {
        _killing = true;
        Destroy(this.gameObject, GetComponentInChildren<TrailRenderer>().time);
    }
}
