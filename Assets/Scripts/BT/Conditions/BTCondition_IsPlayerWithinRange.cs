using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTCondition_IsPlayerWithinRange : BTCondition
{
    public float range = 10f;
    public override int Evaluate(params object[] data)
    {
        float dist = Vector3.Distance(owner.transform.position, GameManager.Instance.player.transform.position);
        return (dist < range ? 1 : 0);
    }
}
