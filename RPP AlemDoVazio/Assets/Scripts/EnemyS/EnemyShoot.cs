using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    // Start is called before the first frame update
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
        //anim.SetTrigger("hit");

        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
    
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().Damage(damage);
        }
    }
}

