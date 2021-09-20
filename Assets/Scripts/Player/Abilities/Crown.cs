using UnityEngine;

public class Crown : MonoBehaviour
{
    private float damageToEnemy;

    private void Awake()
    {
        damageToEnemy = GameObject.Find("Player").GetComponent<Player>().damageToEnemy;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If player, damage
        if (collision.name.Contains("Gomba")) { collision.gameObject.SetActive(false); }
        if (collision.name.Contains("Boss")) { collision.gameObject.GetComponent<Boss>().SetHealth(-damageToEnemy); }
    }
}
