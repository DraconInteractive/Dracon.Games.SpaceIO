using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class EnemyWeaponModule : SerializedMonoBehaviour, IModule
{
    public List<WeaponSet> enemyWeapons = new List<WeaponSet>();
    private Enemy _target;

    public class WeaponSet
    {
        [ValueDropdown("GetListOfTypes")]
        public Type weaponType;
        [InfoBox("Leave this blank to fetch default")]
        public WeaponConfig config;
        
        #if UNITY_EDITOR
        private IEnumerable<Type> GetListOfTypes()
        {
            var weaponType = UnityEditor.TypeCache.GetTypesDerivedFrom<Weapon>();
            return weaponType;
        }
        #endif
    }
    
    public void Register(AICharacter character)
    {
        _target = character as Enemy;
        
        foreach (var weapon in enemyWeapons)
        {
            if (weapon.config == null)
            {
                _target.AddWeapon(weapon.weaponType);
            }
            else
            {
                _target.AddWeapon(weapon.weaponType, weapon.config);
            }
        }
    }

    public void Deregister()
    {
        foreach (var weapon in enemyWeapons)
        {
            _target.RemoveWeapon(weapon.weaponType);
        }
    }
}
