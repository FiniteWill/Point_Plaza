using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;
using Enums;

/// <summary>
/// Universal script for health management on entities. Anything that can take damage uses this.
/// </summary>
[DisallowMultipleComponent]
public class Health : MonoBehaviour
{
    [SerializeField] private bool isDebugging = false;
    [SerializeField] private HealthType healthType = HealthType.Enemy;
    public HealthType HealthType => healthType;
    [SerializeField] private bool isVulnerable = true;
    public bool GetIsVulnerable() { return isVulnerable; }
    [SerializeField] private int health = 0;
    public void SetHealth(int newHealth)
    {
        if(isVulnerable) { HandleHealthChanged(newHealth); }
    }

    public event Action<int> onHealthChanged;
    public event Action onHealthReachedZero;

    public void TakeDamage(int damageToTake)
    {
        if (isVulnerable) { HandleHealthChanged(health - damageToTake); }
    }

    public void GainHealth(int healthToGain)
    {
        HandleHealthChanged(health + healthToGain);
    }

    private void HandleHealthChanged(int newHealth)
    {
        if(newHealth <= 0)
        {
            // Handle death
            health = 0;
            onHealthReachedZero?.Invoke();
            if (isDebugging)
            { Debug.Log($"{gameObject.name} has reached 0 health."); }
        }
        else if(health < newHealth)
        {
            // Handle healing
            onHealthChanged?.Invoke(newHealth - health);
            health = newHealth;
            if (isDebugging)
            { Debug.Log($"{gameObject.name} has gained {newHealth - health} health."); }
        }
        else if(health > newHealth)
        {
            // Handle damage
            onHealthChanged?.Invoke(-(health - newHealth));
            health = newHealth;
            if (isDebugging)
            { Debug.Log($"{gameObject.name} has lost {health - newHealth} health."); }
        }
    }

}
