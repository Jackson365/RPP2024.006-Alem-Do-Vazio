using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEnemy : MonoBehaviour
{
    private Rigidbody2D rig;

    public float speed; // Velocidade do projétil
    public int damage; // Dano causado pelo projétil

    private Transform player; // Referência ao transform do jogador
    private Vector2 direction; // Direção para o jogador

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>(); 
        Destroy(gameObject, 5f); 
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed* Time.deltaTime);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            HealthObserver.TakeDamage(damage);
            Destroy(gameObject); // Destrói o projétil após causar dano
        }
        
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}