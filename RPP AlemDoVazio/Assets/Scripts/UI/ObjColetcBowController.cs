using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjColetcBowController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameController.instance.ActivateBowUI();
            other.GetComponent<PlayerController>().CollectBow();
            
            Destroy(gameObject); 
        }
    }
}

