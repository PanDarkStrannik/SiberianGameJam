using System;

public class HealthManager
{
    public const int maxHealth = 3;

    public event Action<HealthManager> onHealthChanged;

    public int currentHealth { get; private set; } = maxHealth;

    public void ApplyDamage()
    {
        currentHealth--;
        onHealthChanged?.Invoke(this);
        AudioManager.instance.PankScream();
        if (currentHealth <= 0)
        {
            GameManager.instance.uiManager.ShowLoseUI();
        }
    }

}
