using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class BTAction : BTNode
{
    [ReadOnly]
    public bool executing;
    
    public virtual bool CanExecute()
    {
        return true;
    }

    public virtual void Execute(params object[] data)
    {
        executing = true;
        owner.StartCoroutine(ExecutionRoutine(data));
        
    }

    protected virtual IEnumerator ExecutionRoutine(params object[] data)
    {
        Terminate();
        yield break;
    }

    public virtual void Terminate()
    {
        executing = false;
    }
}
