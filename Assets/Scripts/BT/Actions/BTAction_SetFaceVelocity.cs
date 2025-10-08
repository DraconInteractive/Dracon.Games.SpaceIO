using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTAction_SetFaceVelocity : BTAction
{
    public bool newValue = true;
    protected override IEnumerator ExecutionRoutine(params object[] data)
    {
        owner.blackboard.faceVelocity = newValue;
        yield return base.ExecutionRoutine(data);
    }
}
