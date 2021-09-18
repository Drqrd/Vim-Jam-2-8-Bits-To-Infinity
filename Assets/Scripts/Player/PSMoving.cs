using UnityEngine;

public class PSMoving : PlayerState
{
    public PSMoving(Player playerRef) : base(playerRef)
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
        if (playerRef._rigidbody.velocity.x == 0) { playerRef.SetState(new PSIdle(playerRef)); }
        else if (movement.y != 0 && playerRef.IsGrounded) { playerRef.SetState(new PSJumping(playerRef)); }

        playerRef._rigidbody.velocity = new Vector2(movement.x, playerRef._rigidbody.velocity.y);
    }

    public override void OnStateEnter()
    {
        if (playerRef.DebugMovementStates) { Debug.Log("Moving"); }
        playerRef._collider.sharedMaterial = playerRef.NoFriction;
    }

    public override void OnStateExit()
    {

    }
}
