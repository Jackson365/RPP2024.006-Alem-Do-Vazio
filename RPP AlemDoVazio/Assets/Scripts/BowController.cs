using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowController : MonoBehaviour
{
    private Rigidbody2D rig;
    public float speed;

    public int damage;

    public bool isRight;
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 2f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isRight)
        {
            rig.velocity = Vector2.right * speed;
        }
        else
        {
            rig.velocity = Vector2.left * speed;
        }
        
    }

    void OnTriggerEnter2D(Collider2D collison)
    {
        if(collison.gameObject.tag == "Enemy")
        {
            collison.GetComponent<EnemyController>().Damage(damage);
            Destroy(gameObject);
        }
    }
}