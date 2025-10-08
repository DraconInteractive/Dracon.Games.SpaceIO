using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : Manager<EnvironmentManager>
{
    public override void Initialize()
    {
        base.Initialize();
        Register(this);
        Initialized = true;
    }
}
