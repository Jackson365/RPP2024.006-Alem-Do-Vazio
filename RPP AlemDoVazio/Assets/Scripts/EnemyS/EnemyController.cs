using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Atributtes")]
    public float speed;
    public int health;
    public int damage = 1;
    
    [Header("Components")]
    private Rigidbody2D rig;
    private Animator anim;
    
    [Header("Others")]
    private float timer;
    private bool walkRight = true;
    public float walkTime;
    
    public PlayerController _playerController;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
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
            rig.velocity = Vector2.right * speed;
        }
        else
        {
            transform.eulerAngles = new Vector2(0, 0);
            rig.velocity = Vector2.left * speed;
        }
        
    }

    public void Damage(int vida)
    {
        health -= vida;
        anim.SetTrigger("hit");

        if (health == 4)
        {
            speed += 3;
            walkTime += 3;
        }
        
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
