using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTAction_MoveToPlayer : BTAction
{
     protected override IEnumerator ExecutionRoutine(params object[] data)
     {
          owner.blackboard.targetPosition = GameManager.Instance.player.transform.position;
          yield return base.ExecutionRoutine(data);
     }
}
