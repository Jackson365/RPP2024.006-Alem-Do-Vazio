using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapsController : MonoBehaviour
{
    public int damage = 1000;
    public PlayerController _playerController;
    
    public GameObject player;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            HealthObserver.TakeDamage(damage);
            Destroy(player);
        }
    }
}
