using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyShoot : MonoBehaviour
{
    public int health;
    public int damage = 1;
    
    public Transform playerPos;
    public PlayerController _playerController;

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
    public Animator anim;
    
    public Transform nomeImagem; 
    public Vector3 nomeOffset = new Vector3(0, 1.4f, 0);
    
    // Camada para os obstáculos que bloqueiam a visão
    public LayerMask obstacleLayer;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
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
            transform.rotation = Quaternion.Euler(0, 0, 0); 
            rig.velocity = Vector2.right * speedEnemy;
            anim.SetInteger("TransitionShoot", 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            rig.velocity = Vector2.left * speedEnemy;
            anim.SetInteger("TransitionShoot", 0);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, playerPos.position);
        
        if (distance < 5 && PlayerInSight())
        {
            FireShoot();
        }
        
        if (nomeImagem != null)
        {
            nomeImagem.position = transform.position + nomeOffset;
            nomeImagem.rotation = Quaternion.identity;
        }
    }

    private bool PlayerInSight()
    {
        Vector2 directionToPlayer = (playerPos.position - transform.position).normalized;
        float distanceToPlayer = Vector2.Distance(transform.position, playerPos.position);
        
        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer, distanceToPlayer, obstacleLayer);
        
        return hit.collider == null;
    }

    private void FireShoot()
    {
        StartCoroutine("Shoot");
    }

    IEnumerator Shoot()
    {
        tempAtual -= Time.deltaTime; 
        anim.SetInteger("TransitionShoot", 1);
        
        if (tempAtual <= 0)
        {
            GameObject shootEnemy = Instantiate(shoot, firePoint.position, Quaternion.Euler(0f, 0f, -90f));
            tempAtual = tempMax;
            
            yield return new WaitForSeconds(0.3f);
        }
    }
    
    public void Damage(int vida)
    {
        health -= vida;
        anim.SetInteger("TransitionShoot", 2);

        if (health <= 0)
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
            else
            {
                _playerController.isKnockRitgh = false;
            }
        }
    }
}
