using UnityEngine;

public class PSMoving : PlayerState
{
    public PSMoving(Player playerRef) : base(playerRef)
    {
        this.playerRef = playerRef;
    }
    public override void Tick()
    {
        Vector2 movement = playerRef.HandleInput();

        if (playerRef._rigidbody.velocity.x == 0) { playerRef.SetState(new PSIdle(playerRef)); }
        else if (movement.y != 0) { playerRef.SetState(new PSJumping(playerRef)); }

        playerRef._rigidbody.velocity = new Vector2(movement.x, playerRef._rigidbody.velocity.y);
    }

    public override void OnStateEnter()
    {
        Debug.Log("Moving");
        playerRef._collider.sharedMaterial = playerRef.NoFriction;
    }
}
