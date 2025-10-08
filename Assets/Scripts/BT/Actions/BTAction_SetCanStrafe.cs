using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTAction_SetCanStrafe : BTAction
{
    public bool newValue;
    protected override IEnumerator ExecutionRoutine(params object[] data)
    {
        owner.blackboard.canStrafe = newValue;
        yield return base.ExecutionRoutine(data);
    }
}
