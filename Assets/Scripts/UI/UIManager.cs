using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIManager : Manager<UIManager>
{
    public List<UIScreen> screens;

    public UIScreen current;
    
    public override void Initialize()
    {
        base.Initialize();
        Register(this);

        foreach (var screen in screens)
        {
            screen.Initialize();
        }
        
        Initialized = true;
    }

    public void ShowScreen(System.Type screenType)
    {
        var screen = screens.First(x => x.GetType() == screenType);
        
        if (current != null)
        {
            current.Hide();
        }

        current = screen;
        current.Show();
    }
}
