using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTAction_Idle : BTAction
{
    protected override IEnumerator ExecutionRoutine(params object[] data)
    {
        yield return null;
        yield return base.ExecutionRoutine(data);
    }
}
