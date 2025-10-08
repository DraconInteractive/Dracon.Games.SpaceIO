using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class APIManager : Manager<APIManager>
{
    public override void Initialize()
    {
        base.Initialize();
        Register(this);
        Initialized = true;
    }
}
