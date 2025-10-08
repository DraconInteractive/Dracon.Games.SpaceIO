using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTree
{
    [HideInInspector] public bool evaluating;
    public BTNode startNode;
    private AICharacter owner;
    
    public void Initialize(AICharacter _owner)
    {
        owner = _owner;
        
        List<BTNode> open = new List<BTNode>();
        List<BTNode> closed = new List<BTNode>();

        BTNode current;
        open.Add(startNode);

        while (open.Count > 0)
        {
            current = open[0];
            current.Initialize(owner);
            foreach (var child in current.children)
            {
                if (!closed.Contains(child) && !open.Contains(child))
                {
                    open.Add(child);
                }
            }

            closed.Add(current);
            open.Remove(current);
        }
    }

    public void Evaluate()
    {
        owner.StartCoroutine(EvaluationRoutine());
    }

    private IEnumerator EvaluationRoutine()
    {
        evaluating = true;
        BTNode current = startNode;
        while (current.children.Count > 0)
        {
            if (current is BTCondition con)
            {
                var eval = con.Evaluate();
                current = current.children[eval];
            } else if (current is BTAction action)
            {
                action.Execute();
                while (action.executing)
                {
                    yield return null;
                }
                current = current.children[0];
            }
        }
        
        if (current is BTAction afterAction)
        {
            afterAction.Execute();
            while (afterAction.executing)
            {
                yield return null;
            }
        }

        evaluating = false;
        yield break;
    }
}
