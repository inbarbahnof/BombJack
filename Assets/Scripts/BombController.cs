using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
    public GameManager gameManager;
    public Animator animator;
    public PlayerController player;
    public bool isExploaded;
    public BobmActivater activater;

    private AudioSource audio;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
        isExploaded = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Jack") && player.GetCanCollectBombs())
        {
            if (isExploaded)
            {
                gameManager.score += 200;
                activater.isSpecialBomb = false;
            }
            else
            {
                gameManager.score += 100;
            }
            audio.Play();
            gameManager.collectedBobms++;
            gameManager.UpdateScore();
            animator.SetTrigger("expload");
        }
    }

    public void Explode()
    {
        isExploaded = true;
        animator.SetTrigger("activate");
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
