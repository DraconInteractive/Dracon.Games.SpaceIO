using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponConfig config;
    protected List<Character> _targetsInRange;
    protected float cooldown;
    protected bool _startedFiring;

    public bool playerWeapon;

    protected Character _owner;

    public void Initialize(Character owner, WeaponConfig weaponConfig, bool isPlayerWeapon)
    {
        _owner = owner;
        config = weaponConfig;
        playerWeapon = isPlayerWeapon;
    }
    
    void Update()
    {
        if (!_owner.alive) return;

        FindTargets();
        
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
        
        if ((config.autoFire || _startedFiring) && cooldown <= 0 && _targetsInRange.Count > 0)
        {
            cooldown = config.baseCooldown;
            Fire();
        }
    }
    
    public virtual void Fire()
    {
        StartCoroutine(FireRoutine());
    }

    public virtual void StartFiring()
    {
        _startedFiring = true;
    }

    public virtual void StopFiring()
    {
        _startedFiring = false;
    }

    protected virtual IEnumerator FireRoutine()
    {
        yield break;
    }
    
    protected void FindTargets()
    {
        if (playerWeapon)
        {
            // ToList to avoid multiple enumeration I guess?
            _targetsInRange = EnemyManager.Instance.GetEnemiesInRange(transform.position, config.range)
                .OrderBy(x => Vector3.Distance(transform.position, x.transform.position)).Cast<Character>().ToList();
        }
        else
        {
            _targetsInRange = new List<Character>();
            if (GameManager.DistanceToPlayer(transform.position) < config.range)
            {
                _targetsInRange.Add(GameManager.Instance.player);
            }
        }
        
    }
}
