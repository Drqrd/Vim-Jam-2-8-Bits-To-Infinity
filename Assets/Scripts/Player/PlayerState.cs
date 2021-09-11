public abstract class PlayerState
{
    // Player ref
    public Player playerRef;

    public PlayerState(Player playerRef)
    {
        this.playerRef = playerRef;
    }

    public abstract void Tick();

    public virtual void OnStateEnter() { }
    public virtual void OnStateExit() { }
}
