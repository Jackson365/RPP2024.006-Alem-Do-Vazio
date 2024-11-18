using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class HealthObserver
{
    public static event Action<int> currentHealthEvent;
    public static event Action OnTakeDamage;  // Novo evento para dano
    public static event Action OnDeath; // Novo evento para morte

    public static int currentHealth;
    private static int maxHealth = 6; // Define later

    static HealthObserver()
    {
        currentHealth = maxHealth;
        NotifyHealthChange();
    }

    public static void TakeDamage(int amount)
    {
        currentHealth -= amount;
        NotifyHealthChange();
        
        OnTakeDamage?.Invoke();  // Dispara o evento de dano
        
        if (currentHealth <= 0)
        {
            OnDeath?.Invoke();  // Dispara o evento de morte
        }
    }

    public static void Heal(int amount)
    {
        currentHealth += amount;
        NotifyHealthChange();
    }

    public static void ResetHealth()
    {
        currentHealth = maxHealth;
        NotifyHealthChange();
        
    }

    private static void NotifyHealthChange()
    {
        currentHealthEvent?.Invoke(currentHealth);
    }
}