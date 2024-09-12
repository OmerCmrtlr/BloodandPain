using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth;
    public float immunityTime;
    public float currentHealth;

    Animator anim;


    public Image healthBar;

    bool isImmune;


    void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth >= maxHealth) 
        {
            currentHealth = maxHealth;
        }

        healthBar.fillAmount = currentHealth / 100;
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy") && !isImmune) 
        {
            currentHealth -= collision.GetComponent<EnemyStats>().damage;
            StartCoroutine(Immunity());
            anim.SetTrigger("Hit");

            if (currentHealth <= 0) 
            {
                currentHealth = 0;
                Destroy(gameObject);
            }
        }
    }

    //private void OnTriggerStay2D(Collider2D collision)
    //{
        
    //}

    IEnumerator Immunity() 
    {
        isImmune = true;
        yield return new WaitForSeconds(immunityTime);
        isImmune = false;
    }
}
