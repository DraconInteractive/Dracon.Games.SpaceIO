using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class BTNode
{
    [PropertyOrder(1)]
    public List<BTNode> children = new List<BTNode>();
    
    protected AICharacter owner;

    public virtual void Initialize(AICharacter _owner)
    {
        owner = _owner;
    }
}
