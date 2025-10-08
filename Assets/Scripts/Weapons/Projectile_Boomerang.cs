using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Projectile_Boomerang : Projectile
{
    public float checkRadius = 1f;
    public float turnTime = 3f;
    private List<Enemy> _hitEnemies = new List<Enemy>();
    private float currentSpeed;
    
    public override void Launch(WeaponConfig config, Character target = null)
    {
        base.Launch(config, target);
        currentSpeed = _config.speed;
        if (_target != null)
        {
            Vector3 direction = _target.transform.position - transform.position;
            transform.rotation = Quaternion.LookRotation(direction.normalized);
        }
    }

    public override IEnumerator LaunchRoutine()
    {
        for (float f = 0; f < turnTime - 0.5f; f += Time.deltaTime)
        {
            yield return null;
        }

        for (float f = 0; f < 0.5f; f += Time.deltaTime)
        {
            currentSpeed = Mathf.Lerp(_config.speed, 0, f * 2);
        }
        
        _hitEnemies.Clear();
        transform.forward = -transform.forward;
        for (float f = 0; f < 0.5f; f += Time.deltaTime)
        {
            yield return null;
        }
        
        for (float f = 0; f < 0.5f; f += Time.deltaTime)
        {
            currentSpeed = Mathf.Lerp(0,_config.speed, f * 2);
        }
        
        for (float f = 0; f < turnTime - 0.5f; f += Time.deltaTime)
        {
            yield return null;
        }
        
        Kill();
    }

    private void FixedUpdate()
    {
        if (_killing) return;
        transform.position += transform.forward * (currentSpeed * Time.deltaTime);
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
