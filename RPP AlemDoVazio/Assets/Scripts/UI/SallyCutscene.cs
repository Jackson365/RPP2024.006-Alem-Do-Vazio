using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class SallyCutscene : MonoBehaviour
{
    public GameObject LimitadorObj;
    public GameObject LimitadorInicio;
    public GameObject CutsCene;
    public GameObject Personagem;
    public PlayableDirector cutscene; 
    
    private bool jaAtivou = false;
    
    private void Start()
    {
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
            }
        }
    }

    private void OnCutsceneEnd(PlayableDirector director)
    {
        if (director == cutscene)
        {
            // Reativa o controle do player
            LimitadorObj.SetActive(false);
            LimitadorInicio.SetActive(false);
            Destroy(CutsCene);
            Destroy(Personagem);
            
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