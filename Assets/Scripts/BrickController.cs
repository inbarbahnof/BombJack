using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BrickController : MonoBehaviour
{
    public GameObject[] ghostColliders;

    private void Start()
    {
        SetCollidersActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ghost"))
        {
            SetCollidersActive(true);
        }
    }

    private void SetCollidersActive(bool active)
    {
        foreach (var collider in ghostColliders)
        {
            collider.gameObject.SetActive(active);
        }
    }
}
