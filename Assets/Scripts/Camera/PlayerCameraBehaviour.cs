using UnityEngine;

public class PlayerCameraBehaviour : CameraBehaviour
{
    public Vector3 posOffset;
    public Vector3 rotOffset;

    public float moveSpeed;
    
    public override void Toggle(bool state)
    {
        base.Toggle(state);
        gameObject.SetActive(state);
        transform.rotation = Quaternion.Euler(rotOffset);
    }

    public override void Tick()
    {
        base.Tick();
        Vector3 playerPos = GameManager.Instance.player.transform.position;
        Vector3 targetPos = Vector3.MoveTowards(transform.position, playerPos + posOffset, moveSpeed * Time.deltaTime);
        transform.position = targetPos;
    }
}
