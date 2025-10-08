using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Tracking : Projectile
{
    public float checkRadius = 1f;
    public float rotationSpeed = 360f;
    
    public override void Launch(WeaponConfig config, Character target = null)
    {
        base.Launch(config, target);
        if (target == null)
        {
            Debug.LogError("Null target provided to tracker");
            Kill();
        }
    }

    private void FixedUpdate()
    {
        if (_killing) return;
        if (_target == null)
        {
            Kill();
        }
        Vector3 direction = _target.transform.position - transform.position;
        var targetRotation = Quaternion.LookRotation(direction.normalized);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        transform.position += transform.forward * (_config.speed * Time.deltaTime);
        CheckForHit(checkRadius);
    }

    public override void Kill()
    {
        _killing = true;
        Destroy(this.gameObject, GetComponentInChildren<TrailRenderer>().time);
    }
}
