using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 force;

    private Rigidbody2D rb;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        if (rb.velocity.x < 0)
        {
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
    
    private void FixedUpdate()
    {
        rb.velocity = force;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Jack") && !other.CompareTag("Bomb") && !other.CompareTag("Bullet")
            && !other.CompareTag("JackGrounded"))
        {
            Destroy(gameObject);
        }
    }
}
