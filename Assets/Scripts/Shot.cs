using UnityEngine;

public class Shot : MonoBehaviour
{
    private float bulletSpeed = 1f;
    private float damageToPlayer = 25f;
    private Vector3 playerPosition;
    private Vector3 transformPosition;

    private void Awake()
    {
        playerPosition = GameObject.Find("Player").transform.position;
        transformPosition = transform.position;
    }


    private void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().MovePosition(transform.position + (playerPosition - transformPosition).normalized * 2f * bulletSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If player, damage
        if (collision.tag == "Player") { collision.gameObject.GetComponent<Player>().SetHealth(-damageToPlayer); }

        // Destroy self when colliding
        if (collision.name != transform.name && collision.name != "Goomba") { Destroy(transform.gameObject); }
    }
}
