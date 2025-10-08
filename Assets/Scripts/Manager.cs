using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public abstract class Manager : SerializedMonoBehaviour
{
    public abstract void Initialize();
}

public class Manager<T> : Manager where T:Object
{
    private static List<T> _instances;

    public static T Instance => _instances[0];
    public static List<T> Instances => _instances;

    public bool Initialized;
    
    public override void Initialize()
    {
        _instances = new List<T>();
    }

    protected void Register(T obj)
    {
        _instances.Add(obj);
    }

    protected void Deregister(T obj)
    {
        _instances.Remove(obj);
    }
}
