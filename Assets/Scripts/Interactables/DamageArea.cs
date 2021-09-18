using UnityEngine;

public class DamageArea : Area
{
    [Header("Parameters")]
    [SerializeField]
    private float damageToPlayer = 50f;

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) { playerRef.SetHealth(-damageToPlayer); }
    }

}
