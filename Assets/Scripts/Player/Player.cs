using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Player : MonoBehaviour
{
    public float Health { get; protected set; }
    public float InvincibilityTime { get; protected set; }
    public bool IsInvincible { get; protected set; }

    public Rigidbody2D _rigidbody { get; protected set; }
    public CapsuleCollider2D _collider   { get; protected set; }
    public bool IsGrounded { get; protected set; }

    public virtual bool DoubleJump { get; set; }
    public virtual float JumpForce { get; protected set; }
    public virtual float MoveSpeed { get; protected set; }

    public virtual PhysicsMaterial2D NoFriction { get; protected set; }
    public virtual PhysicsMaterial2D Friction   { get; protected set; }

    protected float distToGround;
    Collider2D[] colliders;
    ContactFilter2D filter;


    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider  = GetComponent<CapsuleCollider2D>();
        Health = 100f;
        InvincibilityTime = 2f;

        colliders = new Collider2D[2];
        filter = new ContactFilter2D();
        filter.NoFilter();

        distToGround = _collider.bounds.extents.y / 2f;


        NoFriction = Resources.Load<PhysicsMaterial2D>("PhysicMaterials/NoFriction");
        Friction   = Resources.Load<PhysicsMaterial2D>("PhysicMaterials/Friction");
    }

    protected virtual void Update()
    {
        IsGrounded = CheckIfGrounded();
    }

    public virtual void SetState(PlayerState nextState) { }

    public virtual Vector2 HandleInput() 
    {
        Vector2 v = Vector2.zero;
        return v; 
    }

    // Adds to the health by value
    public void SetHealth(float numToAdd, bool damagedByEnemy = false)
    {
        Health += numToAdd;
        Debug.Log("Health to " + Health);

        if (damagedByEnemy) { StartCoroutine(DamageInvincibilityTimer()); }
    }

    public bool CheckIfGrounded()
    {
        Vector2 center = new Vector2(transform.position.x, transform.position.y - distToGround);
        int colNum = Physics2D.OverlapCircle(center, _collider.bounds.extents.x + 0.1f, filter, colliders);
        return colNum > 1 ? true : false;
    }

    public IEnumerator DamageInvincibilityTimer()
    {
        IsInvincible = true;
        yield return new WaitForSeconds(InvincibilityTime);
        IsInvincible = false;
    }

    protected void OnDrawGizmos()
    {
        Vector2 center = new Vector2(transform.position.x, transform.position.y - GetComponent<CapsuleCollider2D>().bounds.extents.y / 2f);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(center, GetComponent<CapsuleCollider2D>().bounds.extents.x + 0.1f);
    }
}
