using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobmActivater : MonoBehaviour
{
    public GameObject[] bombs;

    private int index = 0;
    private int[] IndexArr = 
        {2, 23, 12, 1, 7, 8, 10, 15, 20, 16, 19, 9, 5, 17, 21, 0, 6, 3, 11, 4, 13, 22, 14, 18};

    public bool isSpecialBomb = false;
    
    private void Start()
    {
        Activate();
    }

    public void Activate()
    {
        Invoke("ActivateABomb", 10f);
    }
    
    public void ResetBombs()
    {
        foreach (var bomb in bombs)
        {
            bomb.gameObject.SetActive(true);
        }
    }

    public void ActivateABomb()
    {
        if (isSpecialBomb == false)
        {
            while (!bombs[IndexArr[index]].gameObject.activeSelf)
            {
                index++;
            }

            GameObject bombObject = bombs[IndexArr[index]];
            BombController bombController = bombObject.GetComponent<BombController>();
            bombController.Explode();
            index++;
            isSpecialBomb = true;
        }
        Invoke("ActivateABomb", 10f);
    }
}
