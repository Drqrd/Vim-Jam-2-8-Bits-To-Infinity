using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goomba : Enemy
{
    public enum GoombaBehavior
    {
        Wander,
        Attack
    }

    public GoombaBehavior myBehavior;

    [SerializeField] private float mySpeed;
    [SerializeField] LayerMask myLayerMask;
    [SerializeField] SpriteRenderer myRenderer;

    [SerializeField] bool myMovingRight = false;

    [SerializeField]
    private float goombaDamage = 50f;

    [SerializeField] Animator myAnimator;

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        myBehavior = GoombaBehavior.Wander;
    }

    void Update()
    {
        switch (myBehavior)
        {
            case GoombaBehavior.Attack:
                break;
            case GoombaBehavior.Wander:

                WanderBehavior();

                break;
        }
    }

    public void WanderBehavior()
    {
        Debug.DrawRay(transform.position, transform.right);
        Debug.DrawRay(transform.position, -transform.right);

        RaycastHit2D RightRay;
        RaycastHit2D LeftRay;
        RightRay = Physics2D.Raycast(transform.position, transform.right, 0.7f, myLayerMask);
        LeftRay = Physics2D.Raycast(transform.position, -transform.right, 0.7f, myLayerMask);

        if (RightRay)
        {
            myRenderer.flipX = false;
            myMovingRight = false;
        }
        if (LeftRay)
        {
            myRenderer.flipX = true;
            myMovingRight = true;
        }

        if (myMovingRight)
        {
            transform.Translate(Vector2.right * mySpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(-Vector2.right * mySpeed * Time.deltaTime);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") 
        { 
            if (!collision.gameObject.GetComponent<Player>().IsInvincible)
            {
                collision.gameObject.GetComponent<Player>().SetHealth(-goombaDamage, true);
            }
        }
    }

    public override void OnHit()
    {
        base.OnHit();
        Destroy(GetComponent<Collider2D>());
        myAnimator.SetTrigger("Death");
        Destroy(gameObject, 2.0f);
    }

    private void OnDestroy()
    {
        RemoveEnemy();
    }
}
