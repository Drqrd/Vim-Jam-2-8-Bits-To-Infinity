using UnityEngine;
using System.Collections;

public class Boss : Enemy
{
    [Header("Parameters")]
    [Range(100f, 500f)] [SerializeField] private float maxHealth   = 200f;
    [Range(.1f, 1f)]    [SerializeField] private float variability = .5f;
    [Range(3f, 6f)] [SerializeField] private float timeBetweenJumps = 4f;
    [Range(1f, 5f)] [SerializeField] private float timeBetweenMove = 2f;
    [Range(1f, 10f)] [SerializeField] private float jumpForce = 4f;
    [Range(1f, 5f)] [SerializeField] private float moveSpeed = 2f;
    [Range(1f, 3f)] [SerializeField] private float moveRange = 2f;
    private Animator anim;
    private Rigidbody2D rb;
    private float health;
    private bool isDying;
    private float jumpTimer, moveTimer;
    private float jumpTime, moveTime;
    private float moveDir;


   

    private void Awake()
    {
        moveDir = -1f;
        health = maxHealth;
        isDying = false;
        rb = GetComponent<Rigidbody2D>();
        anim = transform.Find("BossSprite").GetComponent<Animator>();

        jumpTimer = 0;
        moveTimer = 0;
        StartCoroutine(Move());
    }

    void Start()
    {
        EnemyManager.Instance.myEnemies.Add(this);
    }

    private void Update()
    {
        SetAnimationVariables();

        if (isDying) { Die(); }
    }

    private void FixedUpdate()
    {
        if (moveTimer >= moveTime)
        {
            moveTimer -= moveTime;
            moveTime = RandomizeTimer(timeBetweenMove);
            StopCoroutine(Move());
            StartCoroutine(Move());
        }
        else { moveTimer += Time.fixedDeltaTime; }

        if (jumpTimer >= jumpTime)
        {
            jumpTimer -= jumpTime;
            jumpTime = RandomizeTimer(timeBetweenJumps);
            Jump();
        }
        else { jumpTimer += Time.fixedDeltaTime; }
    }

    private void SetAnimationVariables()
    {
        anim.SetFloat("X", rb.velocity.x);
        anim.SetFloat("Y", rb.velocity.y);
    }

    private IEnumerator Move()
    {
        moveDir = -moveDir;
        while (rb.position.x != rb.position.x + RandomizeDistance())
        {
            rb.velocity = new Vector2(moveSpeed * moveDir, rb.velocity.y);
            yield return new WaitForFixedUpdate();
        }
        yield break;
    }

    private void Jump()
    {
        Debug.Log("Jumping");
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private float RandomizeTimer(float x)
    {
        if (variability > 0f)
        {
            float min = x - variability >= 0f ? x : x - variability;
            return Random.Range(min, x + variability);
        }
        else { return x; }
    }

    private float RandomizeDistance()
    {
        return moveRange * moveDir + Random.Range(-variability, variability);
    }

    private void Die()
    {
        Debug.Log("dead");
        transform.gameObject.SetActive(false);
        RemoveEnemy();
    }

    // Adds to health based on inputted float
    public void SetHealth(float x)
    {
        health += x;
        FMODUnity.RuntimeManager.PlayOneShotAttached("event:/SFX/Enemy/enemy_onDamage", gameObject);

        isDying = health <= 0 ? true : false;
    }
}
