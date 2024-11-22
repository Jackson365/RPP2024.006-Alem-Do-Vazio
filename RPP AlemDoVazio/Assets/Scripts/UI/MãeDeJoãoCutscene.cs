using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class MãeDeJoãoCutscene : MonoBehaviour
{
    public GameObject LimitadorObj;
    public GameObject CutsCene;
    public PlayableDirector cutscene; 
    
    private bool jaAtivou = false;

    public Animator animPlayer;
    public PlayerController playerController; // Referência ao script de controle do player

    private void Start()
    {
        animPlayer = GetComponent<Animator>();
        
        // Associa o método OnCutsceneEnd ao evento stopped
        if (cutscene != null)
        {
            cutscene.stopped += OnCutsceneEnd;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.CompareTag("Player") && !jaAtivou)
            {
                jaAtivou = true;
                cutscene.Play();
                if (playerController != null)
                {
                    playerController.speed = 0;
                    playerController.jumpForce = 0;
                    playerController.isFire = false;
                    playerController.movement = 0;
                }
            }
        }
    }

    private void OnCutsceneEnd(PlayableDirector director)
    {
        if (director == cutscene)
        {
            // Reativa o controle do player
            LimitadorObj.SetActive(false);
            Destroy(CutsCene);
            
            if (playerController != null)
            {
                playerController.speed = 5;
                playerController.jumpForce = 13;
                playerController.isFire = true;
                playerController.movement = 1;
            }
        }
    }

    private void OnDestroy()
    {
        // Remove a assinatura do evento para evitar erros
        if (cutscene != null)
        {
            cutscene.stopped -= OnCutsceneEnd;
        }
    }
}
