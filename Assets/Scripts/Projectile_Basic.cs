using UnityEngine;

public class Projectile_Basic : Projectile
{
    public float checkRadius;
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

    public override void Kill()
    {
        _killing = true;
        Destroy(this.gameObject, GetComponentInChildren<TrailRenderer>().time);
    }
}
