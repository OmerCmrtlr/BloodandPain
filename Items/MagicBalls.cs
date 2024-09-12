using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBalls : MonoBehaviour
{
    public int magicBallAmount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            MagicBallBank.instance.Collect(magicBallAmount);
            Destroy(gameObject);
        }
    }
}
