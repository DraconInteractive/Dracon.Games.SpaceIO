using UnityEngine;
using UnityEngine.VFX;

public class Projectile_Basic : Projectile
{
    public float checkRadius;
    public float killTime = 3;
    public GameObject baseModel;
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
        baseModel.SetActive(false);
        //Destroy(this.gameObject, GetComponentInChildren<TrailRenderer>().time);
        Destroy(this.gameObject, killTime);
    }
}
