using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    
    [Header("Bows")]
    public GameObject[] bows; // Array com prefabs de cada tipo de flecha (Calmaria, Coragem, Desespero)
    private int selectedBowIndex = 0; // Índice da flecha selecionada
    
    void Start()
    {
        rig2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
                
        originalSpeed = speed;
        originalJumpForce = jumpForce;

        selectedBowIndex = 0; 
    }
    
    void Update()
    {
        KnockLogig();
        ChangeBow();
        
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
    
    void ChangeBow()
    {
        // Verifica se o jogador pressionou as teclas de seta para cima ou para baixo
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            selectedBowIndex = (selectedBowIndex + 1) % bows.Length; // Avança para o próximo arco
            GameController.instance.UpdateBowIcon(selectedBowIndex); // Atualiza o ícone da flecha selecionada no GameController
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            selectedBowIndex = (selectedBowIndex - 1 + bows.Length) % bows.Length; // Retrocede para o arco anterior
            GameController.instance.UpdateBowIcon(selectedBowIndex); // Atualiza o ícone da flecha selecionada no GameController
        }
    }
    
    void Bow()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            FireSelectedBow();
        }
    }

    void FireSelectedBow()
    {
        isFire = true;
        anim.SetInteger("Transition", 3);

        GameObject selectedBow = Instantiate(bows[selectedBowIndex], firePoint.position, firePoint.rotation);

        if (transform.rotation.y == 0)
        {
            selectedBow.GetComponent<BowController>().isRight = true;
        }
        else if (transform.rotation.y == 180)
        {
            selectedBow.GetComponent<BowController>().isRight = false;
        }

        StartCoroutine(ResetFireAnimation());
    }

    IEnumerator ResetFireAnimation()
    {
        yield return new WaitForSeconds(0.2f);
        isFire = false;
        anim.SetInteger("Transition", 0);
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
            StartCoroutine(ReduceSpeedTemporarily(3f, 2));
            StartCoroutine(ReduceJumpTemporarily(3f, 3));
            
            //Adicionar o audio de efeito do inimigo
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
