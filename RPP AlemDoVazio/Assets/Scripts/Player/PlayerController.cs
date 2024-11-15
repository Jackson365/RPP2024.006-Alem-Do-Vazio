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
    public float slowDownFactor = 3.5f;
    private float originalSpeed;

    [Header("Bush")] public float BushSpeed = 2f;

    [Header("EnemyFlying")] 
    public float attackFlyingSpeed = 2.5f;
    public bool isFlying;
    private float originalJumpForce;
    
    [Header("KnockBack")] 
    public float kbForce;
    public float kbCount;
    public float kbTime;
    public bool isKnockRitgh;
    
    [Header("EnemyShoot")]
    private bool isParalyzed = false;
    
    void Start()
    {
        rig2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
                
        originalSpeed = speed;
        originalJumpForce = jumpForce;
    }
    
    void Update()
    {
        KnockLogig();
        
        if (!isParalyzed)
        {
            Move();
            Jump();
            Bow();
        }
    }

    void Move()
    {
        if (isParalyzed)
        {
            movement = 0;
            rig2D.velocity = new Vector2(0, rig2D.velocity.y);
            return;
        }

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

        if (movement < 0)
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
        
        if (other.gameObject.CompareTag("FallingPlatform"))
        {
            jumpForce -= 3;
        }
    }
    
    private IEnumerator ParalyzePlayer(float duration)
    {
        isParalyzed = true;
        yield return new WaitForSeconds(duration);
        isParalyzed = false;
    }
    
    private IEnumerator ReduceSpeedTemporarily(float duration, float reducedSpeed)
    {
        speed = reducedSpeed;
        yield return new WaitForSeconds(duration);
        speed = originalSpeed; 
    }
    
    private IEnumerator ReduceJumpTemporarily(float duration, float reducedJump)
    {
        jumpForce = reducedJump;
        yield return new WaitForSeconds(duration);
        jumpForce = originalJumpForce; 
    }
    
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("FallingPlatform"))
        {
            jumpForce += 3;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SlowMud")) 
        {
            speed -= slowDownFactor;
        }

        if (other.gameObject.CompareTag("Bush"))
        {
            speed -= BushSpeed;
        }
        
        if (other.gameObject.CompareTag("ShootEnemy"))
        {
            StartCoroutine(ParalyzePlayer(5f)); 
        }
        
        if (other.CompareTag("EnemyFlying"))
        {
            StartCoroutine(ReduceSpeedTemporarily(3f, 2)); // Reduz a velocidade para 2 por 2 segundos
            StartCoroutine(ReduceJumpTemporarily(3f, 3));
        }
    }
    

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("SlowMud")) 
        {
            speed = originalSpeed; 
        }

        if (other.gameObject.CompareTag("Bush"))
        {
            speed = originalSpeed;
        }
    }
}
