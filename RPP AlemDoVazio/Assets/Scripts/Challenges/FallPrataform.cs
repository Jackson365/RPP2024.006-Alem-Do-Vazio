using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallPlatform : MonoBehaviour
{
    public float timeToFall = 0.5f;
    public float resetTime = 1.5f;
    
    public float timeToTrigger;
    
    private Rigidbody2D _rigidbody2D;
    private Collider2D _collider2D;

    private Vector2 _initialPosition;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _collider2D = GetComponent<Collider2D>();

        _initialPosition = transform.position;
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //other.transform.SetParent(transform);
            Invoke("DropPlatform", timeToFall);
            
            Invoke("EnableTrigger", timeToTrigger);
        }
    }
    
    private void DropPlatform()
    {
        _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        _rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        
        Invoke("ResetPlatform", resetTime);
    }

    private void ResetPlatform()
    {
        _rigidbody2D.bodyType = RigidbodyType2D.Static;
        transform.position = _initialPosition;
        
        _collider2D.isTrigger = false;
    }
    
    private void EnableTrigger()
    {
        _collider2D.isTrigger = true;
    }
}