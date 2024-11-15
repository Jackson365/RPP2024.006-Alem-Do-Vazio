using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrollAnimation : MonoBehaviour
{
    public Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(AnimationAttack());
        }
    }

    private IEnumerator AnimationAttack()
    {
        anim.SetInteger("Collision", 1);
        yield return new WaitForSeconds(1f);
        anim.SetInteger("Collision", 0);
    }
}
