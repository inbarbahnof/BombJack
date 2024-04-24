using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerShooter : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletForce;
    public PlayerController playerController;
    public Animator shootAnimator;
    
    private bool enableShooting = false;
    private Vector2 fireDirection;
    private bool isNotStrike = true;
    private float shootingDuration = 5.0f; 
    private float nextShootTime;

    private void Start()
    {
        // Set the next shooting time initially
        nextShootTime = Time.time + Random.Range(1.0f, 5.0f); 
    }

    private void Update()
    {
        // Check if it's time to enable shooting
        if (Time.time >= nextShootTime && isNotStrike)
        {
            enableShooting = true;
            shootAnimator.SetTrigger("shoot");
        }
        else
        {
            shootAnimator.SetTrigger("notShoot");
        }

        if (Input.GetKeyDown(KeyCode.Space) && enableShooting)
        {
            GameObject go = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Bullet bullet = go.GetComponent<Bullet>();
            fireDirection = playerController.GetBulletVelocity();
            if (fireDirection.x == 0)
            {
                float randomX1 = Random.Range(-3f, -1f);
                float randomX2 = Random.Range(1f, 3f);

                fireDirection.x = Random.value < 0.5f ? randomX1 : randomX2;
            }
            bullet.force = fireDirection * bulletForce;
            
            enableShooting = false;
            nextShootTime = Time.time + shootingDuration;
        }
    }

    public void DisableShooting()
    {
        isNotStrike = false;
    }
    
    public void EnableShooting()
    {
        isNotStrike = true;
    }
}