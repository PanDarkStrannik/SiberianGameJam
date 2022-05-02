using System;
using UnityEngine;

public class HealthManager
{
    public const int maxHealth = 3;

    public int currentHealth { get; private set; } = maxHealth;

    public event Action onDiy;

    public void ApplyDamage()
    {
        currentHealth--;
        if (currentHealth <= 0)
        {
            onDiy?.Invoke();
            Debug.Log("You loose");
        }
    }

}
