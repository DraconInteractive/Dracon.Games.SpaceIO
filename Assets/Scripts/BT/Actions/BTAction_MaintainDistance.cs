using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTAction_MaintainDistance : BTAction
{
    public float distanceToMaintain = 10f;
    protected override IEnumerator ExecutionRoutine(params object[] data)
    {
        Vector3 playerPos = GameManager.Instance.player.transform.position;
        Vector3 dirToEnemy = owner.transform.position - playerPos;
        Vector3 result = playerPos + dirToEnemy.normalized * distanceToMaintain;
        owner.blackboard.targetPosition = result;

        yield return base.ExecutionRoutine(data);
    }
}
