using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float timer;
    void Start()
    {
        Destroy(gameObject,timer);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
