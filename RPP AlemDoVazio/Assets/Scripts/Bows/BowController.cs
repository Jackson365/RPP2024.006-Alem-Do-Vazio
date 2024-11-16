using System.Collections;
using UnityEngine;

public class BowController : MonoBehaviour
{
    protected Rigidbody2D rig;
    public float speed = 10f; // Velocidade padrão da flecha
    public int damage = 1;    // Dano padrão da flecha
    public bool isRight;      // Direção da flecha (true = direita, false = esquerda)

    protected virtual void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 2f); // Destrói a flecha após 2 segundos
    }

    protected virtual void FixedUpdate()
    {
        // Define a direção da velocidade com base em `isRight`
        float direction = isRight ? 1 : -1;
        rig.velocity = new Vector2(direction * speed, rig.velocity.y);
    }

    /*protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.CompareTag("EnemyPatroll"))
        {
            // Aplica o dano ao inimigo
            collision.GetComponent<EnemyFlying>().Damage(damage);
            Destroy(gameObject); // Destroi a flecha ao colidir com o inimigo
        }
    }*/
}