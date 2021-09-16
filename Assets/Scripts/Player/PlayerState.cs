public abstract class PlayerState
{
    // Player ref
    public Player playerRef;
 

    public PlayerState(Player playerRef)
    {
        this.playerRef = playerRef;
    }

    // Update
    public abstract void Tick();

    // Fixed Update
    public abstract void FixedTick();

    public virtual void OnStateEnter() { }
    public virtual void OnStateExit() { }
}
