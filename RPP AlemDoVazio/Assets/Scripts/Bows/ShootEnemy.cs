using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEnemy : MonoBehaviour
{
    [Header("Atributtes")]
    public float speed;
    public int damage;
    
    [Header("Others")]
    private Rigidbody2D rig;
    public bool isRight;
    
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 2f);
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        if (isRight)
        {
            rig.velocity = Vector2.right * speed;
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            rig.velocity = Vector2.left * speed;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        
    }
    
    public void OnTriggerEnter2D(Collider2D collison)
    {
        if (collison != null)
        {
            if (collison.gameObject.tag == "Player")
            {
                HealthObserver.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}

