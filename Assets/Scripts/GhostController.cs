using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GhostController : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isGrounded;
    private Animator animator;
    private bool moveRight = true;
    private bool enableMovement = true;
    private float direction = 1f;
    private Vector3 initialPosition;
    private bool isDeadly = true;
    private bool checkOnCollisionStay = false;
    
    public float speed = 1f;
    public IsGrounded grounded;
    public Collider2D ghostCollider;
    public GameManager gameManager;
    void Start()
    {   
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        initialPosition = transform.position;
        
        int randomNumber = Random.Range(-1, 1);
        if (randomNumber >= 0)
        {
            direction = 1f;
        }
        else
        {
            direction = -1f;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (checkOnCollisionStay)
        {
            checkOnCollisionStay = false;
            if (other.CompareTag("wall"))
            {
                direction = -direction;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Jack") && isDeadly)
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.Strike();
            }
        }
        else if (other.CompareTag("wall"))
        {
            direction = -direction;
        }
        else if (other.CompareTag("final floor") || other.CompareTag("Bullet"))
        {
            if (other.CompareTag("Bullet"))
            {
                gameManager.score += 50;
            }
            enableMovement = false;
            ghostCollider.enabled = false;
            isDeadly = false;
            animator.SetTrigger("expload");
            Invoke("Reset", 1.5f);
            Invoke("MakeDeadly", 2f);
        }
    }

    private void MakeDeadly()
    {
        isDeadly = true;
    }

    private void Update()
    {
        isGrounded = grounded.isGrounded;
        animator.SetBool("grounded", isGrounded);
        
        if (((moveRight && direction < 0f) || (!moveRight && direction > 0f)) && enableMovement)
        {
            moveRight = !moveRight;
            Flip();
        }
    }

    private void FixedUpdate()
    {
        animator.SetFloat("speed", speed);
        if (isGrounded && enableMovement)
        {
            // Move the ghost horizontally
            rb.velocity = new Vector2(speed * direction, rb.velocity.y);
        }
        else if (!enableMovement)
        {
            rb.velocity = Vector2.zero;
        }
    }
    
    private void Flip()
    {
        // set animation to go right/left
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    public void StopMoving()
    {
        gameObject.SetActive(false);
    }

    public void StartMoving()
    {
        gameObject.SetActive(true);
        checkOnCollisionStay = true;
    }

    public void Reset()
    {
        animator.SetTrigger("stop");
        enableMovement = true;
        ghostCollider.enabled = true;
        transform.position = initialPosition;
    }
}
