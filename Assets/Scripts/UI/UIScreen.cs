using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIScreen : MonoBehaviour
{
    public CanvasGroup group;

    public List<UIComponent> components = new List<UIComponent>();
    
    [HideInInspector]
    public UnityAction onShow;
    [HideInInspector]
    public UnityAction onHide;

    public void Initialize()
    {
        foreach (var component in components)
        {
            component.Initialize(this);
        }
    }
    
    public virtual void Show()
    {
        group.alpha = 1;
        group.blocksRaycasts = true;
        onShow?.Invoke();
    }

    public virtual void Hide()
    {
        group.alpha = 0;
        group.blocksRaycasts = false;
        onHide?.Invoke();
    }
}
