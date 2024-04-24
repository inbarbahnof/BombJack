using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GhostCollider : MonoBehaviour
{
    private int collisionNum = 0;
    private int collisionNumUntilDisappear = 4;

    private void Start()
    {
        collisionNumUntilDisappear = Random.Range(2,5);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ghost"))
        {
            collisionNum++;

            if (collisionNum >= collisionNumUntilDisappear)
            {
                collisionNum = 0;
                collisionNumUntilDisappear = Random.Range(2,5);
                gameObject.SetActive(false);
            }
        }
    }
}
