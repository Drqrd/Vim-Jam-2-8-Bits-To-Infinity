using UnityEngine;

public class PSJumping : PlayerState
{
    public PSJumping(Player playerRef) : base(playerRef)
    {
        this.playerRef = playerRef;
    }
    public override void Tick()
    {
        Vector2 movement = playerRef.HandleInput();

        // if not vertically moving
        if (Mathf.Abs(playerRef._rigidbody.velocity.y) == 0f)
        {
            // If there is x velocity, set state to moving, else idle
            if (Mathf.Abs(playerRef._rigidbody.velocity.x) != 0f) { playerRef.SetState(new PSMoving(playerRef)); }
            else { playerRef.SetState(new PSIdle(playerRef)); }
        }

        // Move player
        playerRef._rigidbody.velocity = new Vector3(movement.x, playerRef._rigidbody.velocity.y);
    }

    public override void OnStateEnter()
    {
        Debug.Log("Jumping");
        playerRef._rigidbody.velocity += Vector2.up * playerRef.JumpForce;
    }
}
