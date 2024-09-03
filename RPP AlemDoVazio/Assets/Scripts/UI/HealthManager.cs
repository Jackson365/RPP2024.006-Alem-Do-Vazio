using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public static HealthManager healthManager;
    public Text healthText;

    public GameObject player;
    
    private void Awake()
    {
        healthManager = this;
    }

    private void Start()
    {
        UpdateHealthText(HealthObserver.currentHealth);
    }
    
    private void OnEnable()
    {
        HealthObserver.currentHealthEvent += UpdateHealthText;
        HealthObserver.currentHealthEvent += CheckPlayerHealth;
    }

    private void OnDisable()
    {
        HealthObserver.currentHealthEvent -= UpdateHealthText;
        HealthObserver.currentHealthEvent -= CheckPlayerHealth;
    }

    private void UpdateHealthText(int currentHealth)
    {
        healthText.text = currentHealth.ToString();
    }
    
    private void CheckPlayerHealth(int currentHealth)
    {
        if (currentHealth <= 0)
        {
            DestroyPlayer();
        }
    }

    private void DestroyPlayer()
    {
        AudioObserver.OnStopMusicEvent();
        Destroy(player);
    }
}



