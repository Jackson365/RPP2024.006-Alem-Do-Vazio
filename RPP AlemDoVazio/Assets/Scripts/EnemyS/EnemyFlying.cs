using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class EnemyFlying : MonoBehaviour
{
    [Header("Atributtes")]
    public int health;
    public int damage = 1;
    
    [Header("Components")]
    public Transform playerPos;
    public Rigidbody2D rigFly;
    public SpriteRenderer spriteRenderer;
    private Animator anim;
    
    [Header("Others")]
    public float speedEnemy;
    public float waitingTime;
    public float attackRange = 1f; // Distância máxima para atacar
    public float distance;
    private Vector3 initialPosition;
    
    private int random;
    private float time;
    //private bool isAttacking = false; // Variável para controlar se o inimigo está atacando
    
    // Novo: cooldown do ataque
    public float attackCooldown = 0.5f;  // Tempo entre os ataques
    private float nextAttackTime = 0f; // Próximo tempo permitido para atacar
    
    public Transform nomeImagem; 
    public Vector3 nomeOffset = new Vector3(0, 1.4f, 0);
    
    public PlayerController _playerController;

    public AudioSource attack;


    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        initialPosition = transform.position;

        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        FollowPlayer();
        
        if (nomeImagem != null)
        {
            nomeImagem.position = transform.position + nomeOffset;
            nomeImagem.rotation = Quaternion.identity;
        }
    }

    private void FollowPlayer()
    {
        float distance = Vector2.Distance(transform.position, playerPos.position);
        
        spriteRenderer.flipX = playerPos.position.x < transform.position.x;

        if (distance <= attackRange)
        {
            if (Time.time >= nextAttackTime)
            {
                HealthObserver.TakeDamage(damage);
                nextAttackTime = Time.time + attackCooldown;

                TriggerKnockback();
            }
        }
        else if (distance > attackRange && distance < 4)
        {
            anim.SetInteger("Collision", 1);
            attack.Play();
            transform.position = Vector2.MoveTowards(transform.position, playerPos.position, speedEnemy * Time.deltaTime);
        }
        else if (distance >= 4)
        {
            anim.SetInteger("Collision", 0);
            transform.position = Vector2.MoveTowards(transform.position, initialPosition, speedEnemy * Time.deltaTime);
        }
    }
    
    public void Damage (int vida)
    {
        health -= vida;
        anim.SetTrigger("Hit");

        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
    
    private IEnumerator TriggerHitAnimation()
    {
        anim.SetInteger("Collision", 2);
        yield return new WaitForSeconds(0.2f);
        anim.SetInteger("Collision", 0);
    }
    
    private void TriggerKnockback()
    {
        _playerController.kbCount = _playerController.kbTime;
        _playerController.isKnockRitgh = playerPos.position.x <= transform.position.x;
    }
}

