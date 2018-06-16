using UnityEngine;

/// <summary>
/// Handle health events.
/// </summary>
[RequireComponent(typeof(Health))]
public abstract class HealthHandler : MonoBehaviour
{
    protected Health health;

    /// <summary>
    /// Get the health component.
    /// </summary>
    protected void Awake()
    {
        health = GetComponent<Health>();
        health.OnDamage += TakeDamage;
        health.OnHealDamage += HealDamage;
        health.OnOutOfHealth += OutOfHealth;
    }

    /// <summary>
    /// Take damage event.
    /// </summary>
    /// <param name="damage">Damage taken.</param>
    protected abstract void TakeDamage(float damage);

    /// <summary>
    /// Heal damage event.
    /// </summary>
    /// <param name="damage">Damage healed.</param>
    protected abstract void HealDamage(float damage);

    /// <summary>
    /// Out of health event.
    /// </summary>
    protected abstract void OutOfHealth();
}
