using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class EnemyPatroll : MonoBehaviour
{
    public int health;
    public int damage = 1;
    
    public Transform playerPos;
    public Transform[] _position;

    public float speedEnemy;
    public float waitingTime;
    public float attackRange = 1f; // Distância máxima para atacar

    private int random;
    private float time;
    private bool isAttacking = false; // Variável para controlar se o inimigo está atacando
    
    public Rigidbody2D rig;
    
    void Start()
    {
        random = Random.Range(0, _position.Length);
        time = waitingTime;
        rig = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!isAttacking)
        {
            PatrollRandom();
        }
        else
        {
            FollowPLayer();
        }
    }

    public void PatrollRandom()
    {
        transform.position = Vector2.MoveTowards(transform.position,_position[random].position, speedEnemy * Time.deltaTime);
        float _dist = Vector2.Distance(transform.position, _position[random].position);

        if (_dist <= 0.2f)
        {
            if (time <= 0)
            {
                random = Random.Range(0, _position.Length);
                time = waitingTime; 
            }
            else
            {
                time -= Time.deltaTime;
            }
        }
    }
    
    private void FollowPLayer()
    {
        float distance = Vector2.Distance(transform.position, playerPos.position);

        if (distance <= attackRange)
        {
            // Atacar o jogador
            playerPos.GetComponent<PlayerController>().Damage(damage);
        }
        else if (distance > attackRange && distance < 4)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerPos.position, speedEnemy * Time.deltaTime);
        }
        else
        {
            isAttacking = false;
        }
    }
    
    public void Damage (int vida)
    {
        health -= vida;

        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
    
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isAttacking = true;
        }
    }
}