using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed;
    public float detectionRadius;
    public float waitTime;

    Rigidbody2D rb;
    Animator anim;


    public bool isStatic;
    public bool isWalker;
    public bool isPatroller;
    public bool isWalkingRight;

    public bool waiter;


    bool moveToA, moveToB;
    bool isWaiting;


    public Transform pointA, pointB;
    public Transform wallCheck, groundCheck, gapCheck;
    public bool wallDetected , groundDetected , gapDetected ;

    public LayerMask whatIsGround;

    


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        moveToA = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        gapDetected = !Physics2D.OverlapCircle(gapCheck.position, detectionRadius, whatIsGround);
        wallDetected = Physics2D.OverlapCircle(wallCheck.position, detectionRadius, whatIsGround);
        groundDetected = Physics2D.OverlapCircle(groundCheck.position, detectionRadius, whatIsGround);


        if (gapDetected || wallDetected && groundDetected)
        {
            Flip();
        }




    }

    private void FixedUpdate()
    {
        if (isStatic) 
        {
            anim.SetBool("Idle", true);
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        if (isWalker) 
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            anim.SetBool("Idle", false);
            if (!isWalkingRight) 
            {
                rb.velocity = new Vector2(-speed * Time.deltaTime, rb.velocity.y);
            } else 
            {
                rb.velocity = new Vector2(speed * Time.deltaTime, rb.velocity.y);
            }

        }
        if (isPatroller) 
        {
            
            if (moveToA)
            {
                if (!isWaiting) 
                {
                    rb.velocity = new Vector2(-speed * Time.deltaTime, rb.velocity.y);
                    anim.SetBool("Idle", false);
                }
                
                if (Vector2.Distance(transform.position, pointA.position) < 1f) 
                {
                    if (waiter)
                    {
                        StartCoroutine(Waiting());
                    }
                    
                    Flip();
                    moveToA = false;
                    moveToB = true;
                } 
            }
            if (moveToB)
            {
                if (!isWaiting)
                {
                    rb.velocity = new Vector2(speed * Time.deltaTime, rb.velocity.y);
                    anim.SetBool("Idle", false);
                }
                
                if (Vector2.Distance(transform.position, pointB.position) < 1f)
                {
                    if (waiter) 
                    {
                        StartCoroutine(Waiting()); 
                    }
                    Flip();
                    moveToA = true;
                    moveToB = false;
                }
            }
        }
    }

    IEnumerator Waiting () 
    {
        anim.SetBool("Idle", true);
        isWaiting = true;
        Flip();
        yield return new WaitForSeconds(waitTime);
        isWaiting = false;
        anim.SetBool("Idle", false);
        Flip();

    }

    public void Flip() 
    {
        isWalkingRight = !isWalkingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
