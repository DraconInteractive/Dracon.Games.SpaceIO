using System;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : AICharacter
{
    public override void Kill()
    {
        foreach (var child in GetComponentsInChildren<Renderer>())
        {
            child.enabled = false;
        }
        base.Kill();
    }
}
