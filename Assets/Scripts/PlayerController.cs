using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private float horizontal;
    private float jumpingPower = 9f;
    private bool isFacingRight = true;
    private bool isGrounded;
    private Animator animator;
    private bool jumping;
    private float originalGravityScale;
    private Vector3 initialPosition;
    private bool enabledMovement = true;
    private bool canCollectBombs = true;
    
    public AudioSource jumpAudio;
    public AudioSource strikeAudio;
    public float airGravityScale = 0.45f;
    public float speed = 3f;
    public IsGrounded grounded;
    public GameManager gameManager;
    public bool isVonruble = true;
    public PlayerShooter shooter;
    
    // Start is called before the first frame update
    void Start()
    {   
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        originalGravityScale = rb.gravityScale;
        initialPosition = transform.position;
    }
    
    void Update()
    {
        isGrounded = grounded.isGrounded;
        animator.SetBool("grounded", isGrounded); 
        
        if (enabledMovement)
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            float curSpeed = horizontal;

            if (horizontal < 0)
                curSpeed = -horizontal;
        
            animator.SetFloat("speed", curSpeed);
            animator.SetFloat("velocity", rb.velocity.y);
        
            Flip();

            if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
            {
                jumping = true;
                jumpAudio.Play();
            }
        }
    }

    private void FixedUpdate()
    {
        if (jumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            jumping = false;
        }

        if (enabledMovement)
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = Vector2.down;
        }
        // Adjust gravity scale when falling
        if (!isGrounded && rb.velocity.y < 0)
        {
            rb.gravityScale = airGravityScale;
        }
        else
        {
            rb.gravityScale = originalGravityScale;
        }
    }

    public Vector2 GetBulletVelocity()
    {
        return new Vector2(rb.velocity.x, 0);
    }

    private void Flip()
    {
        if ((isFacingRight && horizontal < 0f) || (!isFacingRight && horizontal > 0f))
        {
            isFacingRight = !isFacingRight;
            // set animation to go right/left
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    public bool GetCanCollectBombs()
    {
        return canCollectBombs;
    }
    
    public void Strike()
    {
        if (isVonruble)
        {
            enabledMovement = false;
            isVonruble = false;
            canCollectBombs = false;
            
            shooter.DisableShooting();
            
            gameManager.lives--;
            gameManager.score -= 50;
            gameManager.UpdateScore();
            gameManager.StopGhostMovement();
            gameManager.RemoveLifeFromBoard();
        
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            strikeAudio.Play();
            animator.SetTrigger("Strike");
            Invoke("MakeStrikeFall", 1.3f);
        }
    }

    private void MakeStrikeFall()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        animator.SetTrigger("StrikeFall"); 
    }

    private void AfterDie()
    {
        if (gameManager.lives > 0)
        {
            Invoke("ResetPosition", 1f);
        }
        else
        {
            gameManager.GameOver();
        }
    }

    public void StopMovement(bool isWon)
    {
        isVonruble = false;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        
        if (isWon)
        {
            animator.SetTrigger("Win");
        }
    }

    public void ResetPosition()
    {
        enabledMovement = true;
        canCollectBombs = true;
        shooter.EnableShooting();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        gameManager.StartGhostMovement();
        
        rb.position = initialPosition;
        animator.SetTrigger("Reset");
        Invoke("MakeVonruble", 2f);
    }

    private void MakeVonruble()
    {
        isVonruble = true;
    }
}
