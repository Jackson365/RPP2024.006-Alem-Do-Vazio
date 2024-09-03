using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyShoot : MonoBehaviour
{
    public int health;
    public int damage = 1;
    
    public Transform playerPos;

    public float distance;
    public float speedEnemy;

    private float timer;
    public float walkTime;

    private bool walkRight = true;
    
    public GameObject shoot;
    public Transform firePoint;
    
    public float tempMax; 
    public float tempAtual; 
    
    public Rigidbody2D rig;
    public Animator animator;

    public PlayerController _playerController;
    
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
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
            transform.eulerAngles = new Vector2(0, 0);
            rig.velocity = Vector2.right * speedEnemy;
            animator.SetInteger("Wait", 0);
        }
        else
        {
            transform.eulerAngles = new Vector2(0, 180);
            rig.velocity = Vector2.left * speedEnemy;
            animator.SetInteger("Wait", 0);
        }
        
    }
    
    // Update is called once per frame
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
        StartCoroutine("Shoot");
    }

    IEnumerator Shoot()
    {
        tempAtual -= Time.deltaTime; 

        if(tempAtual <= 0)
        {
            animator.SetInteger("Wait", 1);
            GameObject shootEnemy = Instantiate(shoot, firePoint.position, Quaternion.Euler(0f, 0f, 90f));
            tempAtual = tempMax;
            
            if (transform.rotation.y == 0)
            {
                shootEnemy.GetComponent<ShootEnemy>().isRight = true;
            }
            if (transform.rotation.y == 180)
            {
                shootEnemy.GetComponent<ShootEnemy>().isRight = false;
            }

            yield return new WaitForSeconds(0.3f);
            animator.SetInteger("Wait", 1);
        }
    }
    
    public void Damage (int vida)
    {
        health -= vida;
        animator.SetInteger("Wait", 2);

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

