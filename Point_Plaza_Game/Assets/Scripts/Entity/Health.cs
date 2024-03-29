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
    private int startingHealth = 0;
    public int GetStartHealth => startingHealth;
    private void Awake()
    {
        startingHealth =  health;   
    }

    public event Action<int> onHealthChanged;
    public event Action onHealthReachedZero;

    /// <summary>
    /// Subtracts absolute value of given value from health if the Health is currently vulnerable.
    /// </summary>
    /// <param name="damageToTake"></param>
    public void TakeDamage(int damageToTake)
    {
        if (isVulnerable) { HandleHealthChanged(health - Math.Abs(damageToTake)); }
    }

    /// <summary>
    /// Adds absolute value of given value to health.
    /// </summary>
    /// <param name="healthToGain"></param>
    public void GainHealth(int healthToGain)
    {
        HandleHealthChanged(health + Math.Abs(healthToGain));
    }
    /// <summary>
    /// Adds given value to health (can be positive or negative).
    /// <param name="delta"</param>
    /// </summary>
    public void ChangeHealth(int delta)
    {
        HandleHealthChanged(health + delta);
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
