using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyPatroll : MonoBehaviour
{
    public int health;
    public int damage = 1;
    
    public Transform playerPos;
    public float distance;
    
    public float speedEnemy;

    private float timer;
    public float walkTime;

    private bool walkRight = true;
    private bool isAttacking;
    
    public Rigidbody2D rig;
    public Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    
    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, playerPos.position);
        
        Follow();
    }

    private void Follow()
    {
        if (distance < 5)
        {
            isAttacking = true;
            
            Vector3 direction = (playerPos.position - transform.position).normalized; // Direção para o jogador
            transform.position += direction * speedEnemy * Time.deltaTime; // Move o inimigo

            anim.SetInteger("WaitP", 1);

            Quaternion lookRotation = Quaternion.LookRotation(playerPos.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * speedEnemy);

        }
            
        //transform.position = Vector2.MoveTowards(transform.position, playerPos.position, speedEnemy * Time.deltaTime);
        /*Vector3 direction = (playerPos.position - transform.position).normalized; // Direção para o jogador
        transform.position += direction * speedEnemy * Time.deltaTime; // Move o inimigo
        
        anim.SetInteger("WaitP", 1);
        
        Quaternion lookRotation = Quaternion.LookRotation(playerPos.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * speedEnemy);*/
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
            isAttacking = false;
            transform.eulerAngles = new Vector2(0, 0);
            rig.velocity = Vector2.right * speedEnemy;
            anim.SetInteger("WaitP", 0);
           
        }
        else
        {
            isAttacking = false;
            transform.eulerAngles = new Vector2(0, 180);
            rig.velocity = Vector2.left * speedEnemy;
            anim.SetInteger("WaitP", 0);
        }
    }
    
    public void Damage (int vida)
    {
        health -= vida;
        anim.SetInteger("WaitP", 2);

        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerController>().Damage(damage);
        }
    }
}