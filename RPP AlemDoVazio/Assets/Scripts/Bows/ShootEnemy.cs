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

    private void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    void FixedUpdate()
    {
        if (isRight)
        {
            rig.velocity = Vector2.right * speed;
        }
        else
        {
            rig.velocity = Vector2.left * speed;
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
