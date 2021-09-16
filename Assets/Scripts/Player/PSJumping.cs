using UnityEngine;

public class PSJumping : PlayerState
{
    public PSJumping(Player playerRef) : base(playerRef)
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
        
        if (playerRef.DoubleJump) { playerRef.SetState(new PSDoubleJumping(playerRef)); }

        // Move player
        playerRef._rigidbody.velocity = new Vector3(movement.x, playerRef._rigidbody.velocity.y);

        
    }

    public override void OnStateEnter()
    { 
        playerRef._rigidbody.velocity = new Vector2(playerRef._rigidbody.velocity.x, playerRef.JumpForce);

        //Play a sound ;)
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Player/player_jump", playerRef.transform.position);
    }
}
