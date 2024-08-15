using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatformController : MonoBehaviour
{
    private float speed = 3;
    private float walkTime = 1.5f;
    
    private bool walkRight = true;
    
    private float timer;
    private Rigidbody2D rig;
    
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }
    
    void FixedUpdate()
    {
        timer += Time.deltaTime;

        if (timer >= walkTime)
        {
            walkRight = !walkRight;
            timer = 0f;
        }

        if (walkRight)
        {
            rig.velocity = Vector2.up * speed;
        }
        else
        {
            rig.velocity = Vector2.down * speed;
        }
    }
    
    
}