using System;
using UnityEngine;

/// <summary>
/// Handles health management and damage.
/// </summary>
public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    public float CurrentHealth { get; set; }

    public event Action<float> OnDamage = delegate { };
    public event Action<float> OnHealDamage = delegate { };
    public event Action OnOutOfHealth = delegate { };

    /// <summary>
    /// Default the current health to full.
    /// </summary>
    private void Awake()
    {
        CurrentHealth = maxHealth;
    }

    /// <summary>
    /// Get the decimal percent of health.
    /// </summary>
    /// <returns></returns>
    public float PercentHealth()
    {
        return CurrentHealth / maxHealth;
    }

    /// <summary>
    /// Take damage and keep it at 0 at the lowest.
    /// </summary>
    /// <param name="damage">Amount of damage to take.</param>
    public void TakeDamage(float damage)
    {
        CurrentHealth = CurrentHealth <= 0 ? 0 : CurrentHealth - damage;
        OnDamage(damage);
        if (OutOfHealth()) OnOutOfHealth();
    }

    /// <summary>
    /// Heal damage and cap a max health.
    /// </summary>
    /// <param name="damage">Amount of damage to heal.</param>
    public void HealDamage(float damage)
    {
        CurrentHealth = CurrentHealth > maxHealth ? maxHealth : CurrentHealth + damage;
        OnHealDamage(damage);
    }

    /// <summary>
    /// Check if out of health.
    /// </summary>
    /// <returns>Current health is less than or equal to 0.</returns>
    public bool OutOfHealth()
    {
        return CurrentHealth <= 0;
    }
}
