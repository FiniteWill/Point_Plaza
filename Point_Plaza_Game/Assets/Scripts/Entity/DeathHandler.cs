using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Enums;

/// <summary>
/// 
/// </summary>
[RequireComponent(typeof(Health))]
[DisallowMultipleComponent]
public class DeathHandler : MonoBehaviour
{
    [SerializeField] private Health health = null;
    [SerializeField] private GameObject baseEntity = null;


    private void Awake()
    {
        Assert.IsNotNull(health, $"{name} does not have a {health.GetType()} but requires one.");
        Assert.IsNotNull(baseEntity, $"{name} does not have a base {baseEntity.GetType()} but requires one.");
        health.onHealthReachedZero += HandleDeath;
    }

    private void OnDisable()
    {
        // Unsubscribe in case health was destroyed before this is disabled.
        health.onHealthReachedZero -= HandleDeath;
    }

    private void HandleDeath()
    {
        // Tell the IEntityAnimator implementation to handle the death Animation.
        IEntityAnimator tempAnimator = GetComponentInChildren<IEntityAnimator>();
        tempAnimator.HandleDeath();

        if (health.HealthType == HealthType.PlatformerPlayer)
        {
            // Stop the player's movement 
            PlatformerPlayer_Movement tempMovement = GetComponentInChildren<PlatformerPlayer_Movement>();

            // Update the player's stats and ...

            
        }
        else if (health.HealthType == HealthType.Enemy)
        {
            // Destroy the entity
            Destroy(baseEntity);
        }
    }
}
