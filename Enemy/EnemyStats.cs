using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Audio;
public class EnemyStats : MonoBehaviour
{
    public float maxHealth;
    public float timer;
    public float knockBackForceX, knockBackForceY;
    public float damage;
    public float expToGive;

    Animator anim;

    public AudioSource enemyHitAS, enemyDeathAS;

    public GameObject deathEffect;
    public Transform player;

    Rigidbody2D rb;


    private float currentHealth;


    HitEffect effect;
    
    void Start()
    {
        currentHealth = maxHealth;
        effect = GetComponent<HitEffect>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    
    void Update()
    {
        
    }

    public void TakeDamage(float tdamage) 
    {
        currentHealth -= tdamage;
        anim.SetTrigger("Hit");
        AudioManager.instance.PlayAudio(enemyHitAS);

        if (player.position.x < transform.position.x) 
        {
            rb.AddForce(new Vector2(knockBackForceX, knockBackForceY), ForceMode2D.Force);
        }
        else
        {
            rb.AddForce(new Vector2(-knockBackForceX, knockBackForceY), ForceMode2D.Force);
        }



        GetComponent<SpriteRenderer>().material = effect.white;
        StartCoroutine(BackToNormal());

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Instantiate(deathEffect, transform.position, transform.rotation);
            AudioManager.instance.PlayAudio(enemyDeathAS);
            Destroy(gameObject);
            Experience.instance.expMod(expToGive);
        }
    }  
    IEnumerator BackToNormal()
    {
        yield return new WaitForSeconds(timer);
        GetComponent<SpriteRenderer>().material = effect.original;
    }
}
