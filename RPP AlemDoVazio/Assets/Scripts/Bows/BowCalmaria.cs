using UnityEngine;

public class BowCalmaria : BowController
{
    protected override void Start()
    {
        base.Start();
        speed = 10f;   // Customiza a velocidade da flecha
        damage = 1;   // Customiza o dano da flecha
    }
    
    public void OnTriggerEnter2D(Collider2D collison)
    {
        if (collison != null)
        {
            if (collison.gameObject.tag == "EnemyDesespero")
            {
                collison.GetComponent<EnemyShoot>().Damage(damage);
                Destroy(gameObject);
            }
        }
    }
}
