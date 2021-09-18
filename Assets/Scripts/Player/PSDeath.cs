using UnityEngine;

public class PSDeath : PlayerState
{
    public PSDeath(Player playerRef) : base(playerRef)
    {
        this.playerRef = playerRef;
    }

    public override void Tick()
    {
        
    }
    public override void FixedTick()
    {
        
    }

    public override void OnStateEnter()
    {
        Debug.Log("Death");
    }
}
