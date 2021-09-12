using UnityEngine;

public class PSDoubleJumping : PlayerState
{
    public PSDoubleJumping(Player playerRef) : base(playerRef)
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
        Debug.Log("Double Jumping");
        playerRef._rigidbody.velocity = new Vector2(playerRef._rigidbody.velocity.x, playerRef.JumpForce);
    }

    public override void OnStateExit()
    {
        playerRef.DoubleJump = false;
    }
}
