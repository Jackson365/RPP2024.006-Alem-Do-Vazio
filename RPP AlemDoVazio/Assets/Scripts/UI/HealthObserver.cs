using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class HealthObserver
{
    public static event Action<int> currentHealthEvent;

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
    }

    public static void Heal(int amount)
    {
        currentHealth += amount;
        NotifyHealthChange();
    }

    private static void NotifyHealthChange()
    {
        currentHealthEvent?.Invoke(currentHealth);
    }
}
