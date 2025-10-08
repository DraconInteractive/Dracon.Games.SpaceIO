using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetDataManager : Manager<AssetDataManager>
{
    public Dictionary<Type, WeaponConfig> weaponConfigs = new Dictionary<Type, WeaponConfig>();

    public override void Initialize()
    {
        base.Initialize();
        Register(this);
        Initialized = true;
    }
}
