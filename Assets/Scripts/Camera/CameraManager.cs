using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : Manager<CameraManager>
{
    public List<CameraBehaviour> cameras = new List<CameraBehaviour>();

    private int currentCamIndex;
    public CameraBehaviour current => cameras[currentCamIndex];
    
    public override void Initialize ()
    {
        base.Initialize();
        Register(this);
        Initialized = true;
    }

    private void Update()
    {
        var cam = current;
        cam.Tick();
    }

    private void FixedUpdate()
    {
        var cam = current;
        cam.FixedTick();    
    }

    private void LateUpdate()
    {
        var cam = current;
        cam.LateTick();    
    }

    public void SelectCamera(int index)
    {
        if (currentCamIndex == index)
        {
            return;
        }
        
        current.Toggle(false);
        currentCamIndex = index;
        current.Toggle(true);
    }
}
