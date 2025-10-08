using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Config/Weapons")]
public class WeaponConfig : ScriptableObject
{
    public float baseDamage = 1;
    public float baseCooldown = 0.5f;
    public float speed = 5f;
    public float range = 10f;
    public bool autoFire = true;
    public GameObject projectile;
}
