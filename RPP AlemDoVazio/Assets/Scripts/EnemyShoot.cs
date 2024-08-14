using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    /*public GameObject shoot; 
    public Transform firePoint;
    

    public float velocidadeDoInimigo;
    public int vidaMaximaDoInimigo;
    public int vidaAtualDoInimigo;
    

    public float tempoMaximoEntreOsLasers; 
    public float tempoAtualDosLasers; 

    public bool inimigoAtirador; 
    public bool inimigoAtivado;



    // Start is called before the first frame update
    void Start()
    {
        inimigoAtivado = false;

        vidaAtualDoInimigo = vidaMaximaDoInimigo;
    }

    // Update is called once per frame
    void Update()
    {
        if(inimigoAtirador == true && inimigoAtivado == true)
        {
            AtirarLaser(); 
        }
        
    }

    public void AtivarInimigo()
    {
        inimigoAtivado = true;
    }
    

    private void AtirarLaser()
    {
        tempoAtualDosLasers -= Time.deltaTime; 

        if(tempoAtualDosLasers <= 0)
        {
            Instantiate(shoot, firePoint.position, Quaternion.Euler(0f, 0f, 90f));
            tempoAtualDosLasers = tempoMaximoEntreOsLasers;
        }
    }

    public void MachucarInimigo(int danoParaReceber)
    {
        vidaAtualDoInimigo -= danoParaReceber;

        if(vidaAtualDoInimigo <= 0)
        {

            Destroy(this.gameObject);
        }
    }
    
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

    public void OnTriggerEnter2D(Collider2D collison)
    {
        if (collison != null)
        {
            if (collison.gameObject.tag == "EnemyPatroll")
            {
                collison.GetComponent<EnemyPatroll>().Damage(damage);
                Destroy(gameObject);
            }
        }
    }*/
}

