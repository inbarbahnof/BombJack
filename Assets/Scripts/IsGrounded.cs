using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsGrounded : MonoBehaviour
{
    public bool isGrounded = false;
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("floor") || other.gameObject.CompareTag("final floor"))
        {
            isGrounded = true;
        }
    }
    
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("floor") || other.gameObject.CompareTag("final floor"))
        {
            isGrounded = false;
        }
    }

}
