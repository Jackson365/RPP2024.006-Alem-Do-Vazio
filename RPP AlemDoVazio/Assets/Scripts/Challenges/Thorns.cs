using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thorns : MonoBehaviour
{
    public int damage = 1;
    public PlayerController _playerController;
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().Damage(damage);
            
            _playerController.kbCount = _playerController.kbTime;
            if (other.transform.position.x <= transform.position.x)
            {
                _playerController.isKnockRitgh = true;
            }
            if (other.transform.position.x > transform.position.x)
            {
                _playerController.isKnockRitgh = false;
            }
        }
    }
}
