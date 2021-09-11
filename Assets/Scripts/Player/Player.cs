using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Player : MonoBehaviour
{
    public Rigidbody2D _rigidbody { get; protected set; }
    public Collider2D _collider   { get; protected set; }

    public virtual float JumpForce { get; protected set; }
    public virtual float MoveSpeed { get; protected set; }

    public virtual PhysicsMaterial2D NoFriction { get; protected set; }
    public virtual PhysicsMaterial2D Friction   { get; protected set; }

    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider  = GetComponent<Collider2D>();

        NoFriction = Resources.Load<PhysicsMaterial2D>("PhysicMaterials/NoFriction");
        Friction   = Resources.Load<PhysicsMaterial2D>("PhysicMaterials/Friction");
    }

    public virtual void SetState(PlayerState nextState) { }

    public virtual Vector2 HandleInput() 
    {
        Vector2 v = Vector2.zero;
        return v; 
    }
}
