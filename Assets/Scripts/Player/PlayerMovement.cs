using System.Collections;
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
    private float moveSpeed = 4f;
    [SerializeField]
    private float dashCheckTime = 0.5f;
    [SerializeField]
    private float dashSpeed = 10f;
    [SerializeField]
    private float dashDuration = 0.5f;

    public override bool DoubleJump { get; set; }
    public override float JumpForce { get { return jumpForce; } }
    public override float MoveSpeed { get { return moveSpeed; } }

    // Dash logic
    private bool dashingLeft  = false;
    private bool dashingRight = false;
    private float dashLerp     = 0f;
    private float dashVal      = 1f;
    private int   doubleTapLeft          = 0;
    private float doubleTapLeftDuration  = 0f;
    private int   doubleTapRight         = 0;
    private float doubleTapRightDuration = 0f;

    private int doubleJumpKey = 0;

    protected override void Awake()
    {
        base.Awake();
        currentState = new PSIdle(this);
    }

    protected override void Update()
    {
        base.Update();

        currentState.Tick();

        DoubleTapCheck();

        DashInterpolation();
    }

    // Physics update
    private void FixedUpdate()
    {
        currentState.FixedTick();
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

        // Dash logic
        if (Input.GetKeyDown(left) && !dashingLeft)  { DashCheck(left); }
        if (Input.GetKeyDown(right) && !dashingRight) { DashCheck(right); }
        
        // Double jump logic
        if (Input.GetKeyDown(jump)) { DoubleJumpCheck(); }

        if (dashingLeft) { movement += Dash(Vector2.left); }
        if (dashingRight) { movement += Dash(Vector2.right); }


        if (movement.x > 0) myPlayerSprite.flipX = true;
        if (movement.x < 0) myPlayerSprite.flipX = false;


        return new Vector2(movement.x * moveSpeed, movement.y * jumpForce);
    }

    private void DashInterpolation()
    {
        if (dashingLeft || dashingRight)
        {
            dashLerp += Time.deltaTime * (1 / dashDuration);
            dashVal = Mathf.Lerp(0f, 1f, dashLerp);
            if (dashVal >= 1) { dashingLeft = false; dashingRight = false; }
        }
        else { dashLerp = 0f; }
    }

    private void DoubleTapCheck()
    {
        if (doubleTapLeftDuration > 0) { doubleTapLeftDuration -= Time.deltaTime; }
        else { doubleTapLeft = 0; }

        if (doubleTapRightDuration > 0) { doubleTapRightDuration -= Time.deltaTime; }
        else { doubleTapRight = 0; }
    }

    private void DashCheck(KeyCode key)
    {
        // Successful double tap
        if (key == left)
        {
            if (doubleTapLeftDuration > 0f && doubleTapLeft == 1) { dashingLeft = true; myPlayerAnimator.SetTrigger("Dash"); }
            // Single tap
            else
            {
                doubleTapLeftDuration = dashCheckTime;
                doubleTapLeft = 1;
            }
        }
        else
        {
            if (doubleTapRightDuration > 0f && doubleTapRight == 1) { dashingRight = true; myPlayerAnimator.SetTrigger("Dash"); }
            else
            {
                doubleTapRightDuration = dashCheckTime;
                doubleTapRight = 1;
            }
        }
    }

    private Vector2 Dash(Vector2 direction)
    { 
        return dashSpeed * DashFunction(dashVal) * direction;
    }

    private float DashFunction(float x)
    {
        return 1 - (1 - Mathf.Exp(4 * x)) / (1 - Mathf.Exp(4));
    }

    private void DoubleJumpCheck()
    {
        if (doubleJumpKey == 1) 
        {
            doubleJumpKey = 0;
            DoubleJump = true; 
        }
        else 
        { 
            doubleJumpKey = 1;
            DoubleJump = false;
        }
    }
}
