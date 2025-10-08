using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIComponent : MonoBehaviour
{
    protected UIScreen _owner;
    
    public virtual void Initialize(UIScreen owner)
    {
        _owner = owner;
        owner.onShow += OnShow;
        owner.onHide += OnHide;
    }

    protected virtual void OnShow()
    {
        
    }

    protected virtual void OnHide()
    {
        
    }
}
