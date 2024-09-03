using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Atributtes")] 
    public float speed = 5;
    public float jumpForce = 8;
    
    private float movement;
    
    [Header("Components")]
    public Transform firePoint;
    private Rigidbody2D rig2D;
    private Animator anim;
    
    [Header("Others")]
    private bool isJumping;
    private bool doubleJump;
    private bool isFire;
    
    [Header("Bows")]
    public GameObject bowCoragem;
    public GameObject bowCalmaria;
    public GameObject bowDesespero;
    
    [Header("SlowMud")] 
    public float slowDownFactor = 0.5f;
    private float originalSpeed;
    
    [Header("KnockBack")] 
    public float kbForce;
    public float kbCount;
    public float kbTime;
    public bool isKnockRitgh;
    
    void Start()
    {
        rig2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
                
        originalSpeed = speed;
    }
    
    void Update()
    {
        KnockLogig();
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

    void KnockLogig()
    {
        if (kbCount < 0)
        {
            Move();
        }
        else
        {
            if (isKnockRitgh == true)
            {
                rig2D.velocity = new Vector2(-kbForce, kbForce);
            }
            
            if (isKnockRitgh == false)
            {
                rig2D.velocity = new Vector2(kbForce, kbForce);
            }
        }

        kbCount -= Time.deltaTime;
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
                ParticleObserver.OnParticleSpawnEvent(transform.position);
                AudioObserver.OnPlaySfxEvent("Jump");
            }
            else
            {
                if (doubleJump)
                {
                    anim.SetInteger("Transition", 2);
                    rig2D.AddForce(new Vector2(0,jumpForce * 2), ForceMode2D.Impulse);
                    doubleJump = false;
                    //ParticleObserver.OnParticleSpawnEvent(transform.position);
                    AudioObserver.OnPlaySfxEvent("Jump");
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
    
    //VÃO ESTÁ EM GAMECONTROLLER
        
    //CHAMAR ISTO EM RECOMEÇAR!
    //AudioObserver.OnPlayMusicEvent();
        
    //CHAMAR EM GAME OVER!
    //AudioObserver.OnStopMusicEvent()
    
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
