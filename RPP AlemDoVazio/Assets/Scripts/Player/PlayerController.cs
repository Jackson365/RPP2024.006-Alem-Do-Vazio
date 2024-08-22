using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int health = 6;
    public float speed = 5;
    public float jumpForce = 8;

    
    public GameObject bowCoragem;
    public GameObject bowCalmaria;
    public GameObject bowDesespero;
    
    public Transform firePoint;
    
    
    private Rigidbody2D rig2D;
    private Animator anim;

    private bool isJumping;
    private bool doubleJump;
    private bool isFire;

    private float movement;
    
    public float slowDownFactor = 0.5f;
    private float originalSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        rig2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
        GameController.instance.UpdateLives(health);
        
        originalSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        Bow(); 
    }

    void Move()
    {
        movement = Input.GetAxis("Horizontal");
        
        rig2D.velocity = new Vector2(movement * speed, rig2D.velocity.y);

        if (movement > 0)
        {
            if (!isJumping)
            {
                anim.SetInteger("Transition", 1);    
            }
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        if (movement < 0 )
        {
            if (!isJumping)
            {
                anim.SetInteger("Transition", 1);    
            }
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        if (movement == 0 && !isJumping && !isFire)
        {
            anim.SetInteger("Transition", 0);
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (!isJumping)
            {
                anim.SetInteger("Transition", 2);
                rig2D.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                doubleJump = true;
                isJumping = true;
            }
            else
            {
                if (doubleJump)
                {
                    anim.SetInteger("Transition", 2);
                    rig2D.AddForce(new Vector2(0,jumpForce * 2), ForceMode2D.Impulse);
                    doubleJump = false;

                }
            }
        }
    }

    void Bow()
    {
        StartCoroutine("Fire");
        StartCoroutine("FireCALM");
        StartCoroutine("FireDesespero");
    }

    IEnumerator Fire()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        { 
            if(movement == 0)
            {
                isFire = true; 
            
                anim.SetInteger("Transition", 3);
                GameObject BowCoragem = Instantiate(bowCoragem, firePoint.position, firePoint.rotation);

                if (transform.rotation.y == 0)
                {
                    BowCoragem.GetComponent<BowSolidao>().isRight = true;
                }
                if (transform.rotation.y == 180)
                {
                    BowCoragem.GetComponent<BowSolidao>().isRight = false;
                }

                yield return new WaitForSeconds(0.2f);
                isFire = false;
                anim.SetInteger("Transition", 0);
            }
        }
    }

    IEnumerator FireCALM()
    {
        if (Input.GetKeyDown(KeyCode.X))
        { 
            if(movement == 0)
            {
                isFire = true; 
            
                anim.SetInteger("Transition", 3);
                GameObject BowCalmaria = Instantiate(bowCalmaria, firePoint.position, firePoint.rotation);

                if (transform.rotation.y == 0)
                {
                    BowCalmaria.GetComponent<BowCalmaria>().isRight = true;
                }
                if (transform.rotation.y == 180)
                {
                    BowCalmaria.GetComponent<BowCalmaria>().isRight = false;
                }

                yield return new WaitForSeconds(0.2f);
                isFire = false;
                anim.SetInteger("Transition", 0);
            }
        }
    }
    
    IEnumerator FireDesespero()
    {
        if (Input.GetKeyDown(KeyCode.C))
        { 
            if(movement == 0)
            {
                isFire = true; 
            
                anim.SetInteger("Transition", 3);
                GameObject BowDesespero = Instantiate(bowDesespero, firePoint.position, firePoint.rotation);

                if (transform.rotation.y == 0)
                {
                    BowDesespero.GetComponent<BowDesespero>().isRight = true;
                }
                if (transform.rotation.y == 180)
                {
                    BowDesespero.GetComponent<BowDesespero>().isRight = false;
                }

                yield return new WaitForSeconds(0.2f);
                isFire = false;
                anim.SetInteger("Transition", 0);
            }
        }
    }
    
    public void Damage(int dmg)
    {
        health -= dmg;
        GameController.instance.UpdateLives(health);
        anim.SetTrigger("hit");
        
        if (transform.rotation.y == 0)
        {
            transform.position += new Vector3(-1, 0, 0);
        }
                    
        if (transform.rotation.y == 180)
        {
            transform.position += new Vector3(1, 0, 0);
        }
                    
        if(health <= 0)
        {
            //GameController.instance.GameOver();
            Destroy(gameObject);
        }
    }
    
    public void IncreaseLife(int value)
    {
        health += value;
        GameController.instance.UpdateLives(health);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == 8)
        {
            isJumping = false;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SlowMud")) 
        {
            speed *= slowDownFactor; 
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("SlowMud")) 
        {
            speed = originalSpeed; 
        }
    }
}
