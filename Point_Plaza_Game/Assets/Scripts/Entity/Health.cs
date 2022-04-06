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
    public bool GetIsVulnerable() { return isVulnerable; }
    [SerializeField] private int health = 0;
    public void SetHealth(int newHealth)
    {
        if(!isVulnerable)
        {
            HandleHealthChanged(newHealth);
        }
    }

    private void HandleHealthChanged(int newHealth)
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
