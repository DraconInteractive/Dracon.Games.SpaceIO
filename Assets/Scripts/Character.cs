using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public abstract class Character : SerializedMonoBehaviour
{
    [Header("Character - Base")]
    public float currentHealth;
    public float maxHealth;
    
    public bool alive;
    
    [HideInInspector]
    public UnityAction<float> onDamaged;

    [HideInInspector] 
    public UnityAction<float> onHeal;
    
    [HideInInspector]
    public UnityAction<Character> onDeath;
    [HideInInspector]
    public UnityAction onWeaponUpdate;

    private Vector3 _lastFramePos;
    private Vector3 _velocity;
    
    public List<Weapon> weapons { get; private set; }

    protected Transform _cachedTransform;
    public virtual void Initialize()
    {
        _cachedTransform = this.transform;
        weapons = new List<Weapon>();
        currentHealth = maxHealth;
        alive = true;
    }

    private void Update()
    {
        _velocity = transform.position - _lastFramePos;
    }

    private void LateUpdate()
    {
        _lastFramePos = transform.position;
    }

    public virtual void Heal(float incoming)
    {
        currentHealth += incoming;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        onHeal?.Invoke(incoming);
    }

    public virtual void Damage(float incoming)
    {
        currentHealth -= incoming;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        if (currentHealth == 0)
        {
            Kill();
        }
        else
        {
            onDamaged?.Invoke(incoming);
        }
    }

    public virtual void Kill()
    {
        alive = false;
        onDeath?.Invoke(this);
    }

    #region Weapon

    public void AddWeapon<T>() where T : Weapon
    {
        var config = AssetDataManager.Instance.weaponConfigs[typeof(T)];
        AddWeapon<T>(config);
    }
    
    public void AddWeapon(Type weaponType)
    {
        var config = AssetDataManager.Instance.weaponConfigs[weaponType];
        AddWeapon(weaponType, config);
    }
    
    public void AddWeapon<T>(WeaponConfig config) where T : Weapon
    {
        var newWeapon = gameObject.AddComponent<T>();
        newWeapon.Initialize(this, config, (this is Player));
        AddWeapon(newWeapon);
    }
    
    public void AddWeapon(Type weaponType, WeaponConfig config)
    {
        var newWeapon = gameObject.AddComponent(weaponType) as Weapon;
        newWeapon.Initialize(this, config, (this is Player));

        AddWeapon(newWeapon);
    }
    
    public void AddWeapon(Weapon newWeapon)
    {
        weapons.Add(newWeapon);
        onWeaponUpdate?.Invoke();
    }

    public void RemoveWeapon(Type weaponType)
    {
        var weapon = weapons.FirstOrDefault(x => x.GetType() == weaponType);
        weapons.Remove(weapon);
        Destroy(weapon);
        onWeaponUpdate?.Invoke();
    }
    
    public void RemoveWeapon<T>() where T : Weapon
    {
        var weapon = weapons.FirstOrDefault(x => x is T);
        weapons.Remove(weapon);
        Destroy(weapon);
        onWeaponUpdate?.Invoke();
    }

    public void RemoveAllWeapons()
    {
        foreach (var weapon in weapons)
        {
            Destroy(weapon);
        }
        weapons.Clear();
        onWeaponUpdate?.Invoke();
    }

    #endregion
    
}
