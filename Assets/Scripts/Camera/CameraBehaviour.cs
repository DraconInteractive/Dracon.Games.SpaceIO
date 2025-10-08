using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    protected Camera _camera;
    private bool _initialized;
    
    public virtual void Initialize()
    {
        _camera = GetComponent<Camera>();
        _initialized = true;
    }
    
    public virtual void Toggle(bool state)
    {
        if (state && !_initialized)
        {
            Initialize();
        }
    }

    public virtual void Tick()
    {
        if (!_initialized)
        {
            return;
        }
    }

    public virtual void FixedTick()
    {
        if (!_initialized)
        {
            return;
        }
    }

    public virtual void LateTick()
    {
        if (!_initialized)
        {
            return;
        }
    }
}
