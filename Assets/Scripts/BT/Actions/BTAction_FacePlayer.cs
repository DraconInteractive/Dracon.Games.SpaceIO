using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTAction_FacePlayer : BTAction
{
    protected override IEnumerator ExecutionRoutine(params object[] data)
    {
        Player player = GameManager.Instance.player;
        Quaternion current = owner.transform.rotation;
        Quaternion target =
            Quaternion.LookRotation((player.transform.position - owner.transform.position).normalized);
        owner.transform.rotation = Quaternion.RotateTowards(current, target, owner.config.rotationSpeed * Time.fixedDeltaTime);
        yield return base.ExecutionRoutine(data);
    }
}
