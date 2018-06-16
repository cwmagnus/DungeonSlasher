using System.Collections;
using UnityEngine;

/// <summary>
/// Handle enemy health events.
/// </summary>
[RequireComponent(typeof(Animator))]
//[RequireComponent(typeof(EnemyMovement))]
public class EnemyHealthHandler : HealthHandler
{
    [SerializeField] private Color flashColor;
    [SerializeField] private float flashDuration;
    private Color originalColor;
    private Animator animator;
    private SkinnedMeshRenderer meshRenderer;

    /// <summary>
    /// Set local variables.
    /// </summary>
    private void Start()
    {
        animator = GetComponent<Animator>();
        meshRenderer = transform.GetComponentInChildren<SkinnedMeshRenderer>();
        originalColor = meshRenderer.material.color;
    }

    /// <summary>
    /// Check if the enemy is dead.
    /// </summary>
    private void Update()
    {
        // Destroy if dead
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Dead"))
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Take damage event.
    /// </summary>
    /// <param name="damage">Damage taken.</param>
    protected override void TakeDamage(float damage)
    {
        StartCoroutine("FlashHit");
        animator.SetTrigger("Hit");
    }

    /// <summary>
    /// Heal damage event.
    /// </summary>
    /// <param name="damage">Damaged healed.</param>
    protected override void HealDamage(float damage)
    {

    }

    /// <summary>
    /// Out of health event.
    /// </summary>
    protected override void OutOfHealth()
    {
        animator.SetTrigger("Die");
    }

    /// <summary>
    /// Flash animation on hit.
    /// </summary>
    /// <returns>Null every cycle.</returns>
    private IEnumerator FlashHit()
    {
        // How long to flash
        float flashTime = flashDuration;

        // Delay by flash time
        while (flashTime > 0)
        {
            // Flash on hit
            meshRenderer.material.color = flashColor;
            flashTime -= Time.deltaTime;

            yield return null;
        }

        // Revert to original color
        meshRenderer.material.color = originalColor;
    }
}