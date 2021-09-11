using UnityEngine;

public class PlayerMovement : Player
{
    public PlayerState currentState;

    [Header("Keybinds")]
    [SerializeField]
    private KeyCode left  = KeyCode.A;
    [SerializeField]
    private KeyCode right = KeyCode.D;
    [SerializeField]
    private KeyCode jump  = KeyCode.Space;

    [Header("Parameters")]
    [SerializeField]
    private float jumpForce = 10f;
    [SerializeField]
    private float moveSpeed = 10f;

    public override float JumpForce { get { return jumpForce; } }
    public override float MoveSpeed { get { return moveSpeed; } }

    protected override void Awake()
    {
        base.Awake();
        currentState = new PSIdle(this);
    }

    // Physics update
    private void FixedUpdate()
    {
        currentState.Tick();
    }

    // Change state function
    public override void SetState(PlayerState nextState)
    {
        // If there is a current state, call exit method
        if (currentState != null) { currentState.OnStateExit(); }

        // Assign next state
        currentState = nextState;

        // If there is a current state, call enter method
        if (currentState != null) { currentState.OnStateEnter(); }
    }

    // Handles the inputs
    public override Vector2 HandleInput()
    {
        Vector2 movement = Vector2.zero;

        if (Input.GetKey(left))  { movement += Vector2.left; }
        if (Input.GetKey(right)) { movement += Vector2.right; }
        if (Input.GetKey(jump))  { movement += Vector2.up; }

        return new Vector2(movement.x * moveSpeed, movement.y * jumpForce);
    }
}
