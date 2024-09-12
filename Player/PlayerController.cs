using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerController : MonoBehaviour
{
    private float movementDirection;

    public float attackCount;
    public float jumpCount;

    public float lastAttackTime;
    public float speed;
    public float jumpPower;
    public float groundCheckRadius;
    public float attackRate = 2f;
    public float damage;
    float nextAttack = 0;

    public AudioSource swordAS, PlayerDeathVoiceAS;

    private bool isFacingRight = true;
    private bool isGrounded;

    public GameObject bloodEffect;
    public GameObject groundCheck;
    public LayerMask groundLayer;

    public Transform attackPoint;
    public float attackDistance;
    public LayerMask enemyLayers;

    Rigidbody2D rb;
    Animator anim;

    public GameObject magicBall;
    public Transform firePoint;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        CheckRotation();
        Jump();
        CheckSurface();
        CheckAnimations();
        Throw();

        if (Time.time > nextAttack)
        {
            if (Input.GetMouseButton(0))
            {
                Attack();
                nextAttack = Time.time + 1f / attackRate;
                attackCount++;
                lastAttackTime = Time.time;
            }
        }

        if (Time.time - lastAttackTime >= 2f && attackCount > 0 && !Input.GetMouseButton(0))
        {
            attackCount = 0;
        }
    }

    private void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        movementDirection = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(movementDirection * speed, rb.velocity.y);
        anim.SetFloat("runSpeed", Mathf.Abs(movementDirection * speed));
    }

    public void Throw()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (MagicBallBank.instance.magicBank > 0)
            {
                Instantiate(magicBall, firePoint.position, firePoint.rotation);
                MagicBallBank.instance.magicBank -= 1;
                PlayerPrefs.SetInt("MagicBallAmount",MagicBallBank.instance.magicBank);
            }
        }
    }

    void CheckAnimations()
    {
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("yVelocity", rb.velocity.y);
    }

    void CheckRotation()
    {
        if (isFacingRight && movementDirection < 0)
        {
            Flip();
        }
        else if (!isFacingRight && movementDirection > 0)
            Flip();
        {

        }
    }

    void CheckSurface()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.transform.position, groundCheckRadius, groundLayer);
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void Jump()
    {
        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                jumpCount = 0;
                rb.velocity = new Vector2(rb.velocity.x, jumpPower);
                jumpCount++;
            }
        }
        else if (!isGrounded && jumpCount == 1)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpPower);
                jumpCount--;
            }
        }
    }

    public void Attack()
    {
        float numb = Random.Range(0, 2);
        if (numb == 0)
        {
            anim.SetTrigger("Attack1");
            AudioManager.instance.PlayAudio(swordAS);
        }
        if (numb == 1)
        {
            anim.SetTrigger("Attack2");
            AudioManager.instance.PlayAudio(swordAS);
        }

      
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackDistance, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyStats>().TakeDamage(damage);
            Instantiate(bloodEffect, enemy.transform.position, enemy.transform.rotation);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.transform.position, groundCheckRadius);
        Gizmos.DrawWireSphere(attackPoint.position, attackDistance);
    }
}
