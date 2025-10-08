using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCameraBehaviour : CameraBehaviour
{
    public Vector3 targetPos;
    public Vector3 targetRot;
    
    public override void Toggle(bool state)
    {
        base.Toggle(state);
        gameObject.SetActive(state);
        transform.position = targetPos;
        transform.rotation = Quaternion.Euler(targetRot);
    }
}
