using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Tooltip("Time To Live")]
    public float ttl;
    
    protected Character _target;
    protected WeaponConfig _config;

    protected bool _killing;

    protected bool _targetingPlayer;
    
    public virtual void Launch(WeaponConfig config, Character target = null)
    {
        _config = config;
        _target = target;
        _targetingPlayer = (target is Player);
        if (_targetingPlayer)
        {
            _target = GameManager.Instance.player;
        }
        StartCoroutine(LaunchRoutine());
    }

    public virtual IEnumerator LaunchRoutine()
    {
        while (ttl > 0)
        {
            ttl -= Time.deltaTime;
            yield return null;
        }

        if (!_killing)
        {
            Kill();
        }
    }

    public virtual void HitEnemy(Enemy hitEnemy)
    {
        hitEnemy.Damage(_config.baseDamage);
        Kill();
    }

    public virtual void Kill()
    {
        _killing = true;
        Destroy(this.gameObject);
    }

    protected virtual void CheckForHits(float checkRadius)
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
                foreach (var enemy in hitEnemies)
                {
                    HitEnemy(enemy);
                }
            }
        }
    }

    protected virtual void CheckForHit(float checkRadius)
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
            var hitEnemy = EnemyManager.GetClosestInRange(transform.position, checkRadius);
            if (hitEnemy != null)
            {
                HitEnemy(hitEnemy);
            }
        }
    }
}
