using UnityEngine;

public class KillArea : Area
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) { playerRef.SetState(new PSDeath(playerRef)); }
    }
}
