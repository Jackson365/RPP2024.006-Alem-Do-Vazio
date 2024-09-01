using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleHeart : MonoBehaviour
{
    public int vHeart;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            HealthObserver.Heal(vHeart);
            
            other.gameObject.GetComponent<PlayerController>().IncreaseLife(vHeart);
            Destroy(gameObject);
        }
    }
}
