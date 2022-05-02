using System;

public class HealthManager
{
    public const int maxHealth = 3;

    public Action<HealthManager> onHealthChanged;

    public int currentHealth { get; private set; } = maxHealth;

    public void ApplyDamage()
    {
        currentHealth--;
        onHealthChanged?.Invoke(this);
        if (currentHealth <= 0)
        {
            GameManager.instance.uiManager.ShowLoseUI();
        }
    }

}
