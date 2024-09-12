using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBall : MonoBehaviour
{

    public float speed;
    Rigidbody2D rb;
    public float damage;

    public GameObject groundEffect;
    public PlayerController player;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerController>();
        if (player.transform.localScale.x < 0)
        {
            speed = -speed;
        }

    }
    void Update()
    {
        rb.velocity = new Vector2(speed,rb.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyStats>().TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Ground"))
        {
            Instantiate(groundEffect,transform.position,transform.rotation);
            Destroy(gameObject);
        }
    }
}
