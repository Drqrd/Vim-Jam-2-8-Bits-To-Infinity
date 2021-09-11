using UnityEngine;

public class PSIdle : PlayerState
{
    public PSIdle(Player playerRef) : base(playerRef) 
    {
        this.playerRef = playerRef;
    }
    public override void Tick()
    {
        // Change states checks
        if (Mathf.Abs(playerRef.HandleInput().x) > 0f) { playerRef.SetState(new PSMoving(playerRef)); }
        if (Mathf.Abs(playerRef.HandleInput().y) > 0f) { playerRef.SetState(new PSJumping(playerRef)); }
    }

    public override void OnStateEnter()
    {
        Debug.Log("Idle");
        playerRef._collider.sharedMaterial = playerRef.Friction;
    }
}
