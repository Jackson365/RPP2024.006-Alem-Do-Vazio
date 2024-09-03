using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [Header("Atributtes")]
    public int health;
    public int damage = 1;
    public float distance;
    public float speedEnemy;
    
    [Header("Components")]
    public Transform playerPos;
    public GameObject shoot;
    public Transform firePoint;
    public Rigidbody2D rig;

    [Header("Others")]
    private float timer;
    public float walkTime;
    private bool walkRight = true;
    public float tempMax; 
    public float tempAtual; 
    
    public PlayerController _playerController;
   
    void Start()
    {
        rig.GetComponent<Rigidbody2D>();
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
            transform.eulerAngles = new Vector2(0, 180);
            rig.velocity = Vector2.right * speedEnemy;
        }
        else
        {
            transform.eulerAngles = new Vector2(0, 0);
            rig.velocity = Vector2.left * speedEnemy;
        }
        
    }
   
    void Update()
    {
        distance = Vector2.Distance(transform.position, playerPos.position);

        if (distance < 5)
        {
            FireShoot();
        }
    }

    private void FireShoot()
    {
        tempAtual -= Time.deltaTime; 

        if(tempAtual <= 0)
        {
            Instantiate(shoot, firePoint.position, Quaternion.Euler(0f, 0f, 90f));
            tempAtual = tempMax;
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
            HealthObserver.TakeDamage(damage);
            
            _playerController.kbCount = _playerController.kbTime;
            if (collision.transform.position.x <= transform.position.x)
            {
                _playerController.isKnockRitgh = true;
            }
            if (collision.transform.position.x > transform.position.x)
            {
                _playerController.isKnockRitgh = false;
            }
        }
    }
}

