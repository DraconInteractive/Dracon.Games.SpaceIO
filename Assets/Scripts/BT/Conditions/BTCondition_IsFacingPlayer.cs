using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTCondition_IsFacingPlayer : BTCondition
{
    public float angleAllowance = 1f;
    public override int Evaluate(params object[] data)
    {
        Player player = GameManager.Instance.player;
        Quaternion current = owner.transform.rotation;
        Quaternion target =
            Quaternion.LookRotation((player.transform.position - owner.transform.position).normalized);
        return (Quaternion.Angle(current, target) < angleAllowance) ? 1 : 0;
    }
}
