using UnityEngine;

public abstract class Area : MonoBehaviour
{
    protected Player playerRef;

    protected void Awake()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision) { }
    protected virtual void OnTriggerExit2D(Collider2D collision) { }
}
