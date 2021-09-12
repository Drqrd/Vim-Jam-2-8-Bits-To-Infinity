using UnityEngine;

public class PSIdle : PlayerState
{
    public PSIdle(Player playerRef) : base(playerRef) 
    {
        this.playerRef = playerRef;
    }

    private Vector2 movement;

    public override void Tick()
    {
        movement = playerRef.HandleInput();
    }

    public override void FixedTick()
    {
        // Change states checks
        if (Mathf.Abs(movement.x) > 0f) { playerRef.SetState(new PSMoving(playerRef)); }
        if (Mathf.Abs(movement.y) > 0f && playerRef.IsGrounded) { playerRef.SetState(new PSJumping(playerRef)); }
    }

    public override void OnStateEnter()
    {
        Debug.Log("Idle");
        playerRef._collider.sharedMaterial = playerRef.Friction;
    }
}
