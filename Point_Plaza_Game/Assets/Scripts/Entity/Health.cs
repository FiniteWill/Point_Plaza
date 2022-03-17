using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

/// <summary>
/// Universal script for health management on entities. Anything that can take damage uses this.
/// </summary>
public class Health : MonoBehaviour
{
    [SerializeField] private bool isDebugging = false;
    [SerializeField] private bool isVulnerable = true;
    [SerializeField] public int health
    {
        get => health;
        set 
        {
            if(isVulnerable)
            {
                // Value changed, fire event
                OnHealthChanged.Invoke();
                newHealth = value;
            }
        }
    }

    private int newHealth = 0;
    public UnityEvent OnHealthChanged = new UnityEvent();

    public bool GetIsVulnerable() { return isVulnerable; }

    private void Awake()
    {
        // Subscribe to OnHealthChanged event, call HandleHealthChanged() upon event firing.
        OnHealthChanged.AddListener(HandleHealthChanged);
    }

    private void OnDestroy()
    {
        OnHealthChanged.RemoveListener(HandleHealthChanged);
    }

    private void HandleHealthChanged()
    {
        if(newHealth == 0)
        {
            // Handle death
            health = 0;
            if (isDebugging)
            { Debug.Log($"{gameObject.name} has reached 0 health."); }
        }
        else if(health < newHealth)
        {
            // Handle healing
            health = newHealth;
            if (isDebugging)
            { Debug.Log($"{gameObject.name} has gained {newHealth - health} health."); }
        }
        else if(health > newHealth)
        {
            // Handle damage
            health = newHealth;
            if (isDebugging)
            { Debug.Log($"{gameObject.name} has lost {health - newHealth} health."); }
        }
    }
}
