using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public static HealthManager healthManager;

    public Text healthText;

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
    }

    private void OnDisable()
    {
        HealthObserver.currentHealthEvent -= UpdateHealthText;
    }

    private void UpdateHealthText(int currentHealth)
    {
        healthText.text = currentHealth.ToString();
    }
}



