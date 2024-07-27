using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_MoveController : MonoBehaviour
{
    public float speed = 5;
    public float jumpForce;

    private Rigidbody2D rig2D;
    private Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        rig2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        float movement = Input.GetAxis("Horizontal");
        
        rig2D.velocity = new Vector2(movement * speed, rig2D.velocity.y);

        if (movement > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        if (movement < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    void Jump()
    {
        
    }
}
