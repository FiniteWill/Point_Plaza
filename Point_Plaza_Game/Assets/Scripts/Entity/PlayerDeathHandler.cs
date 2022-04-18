using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Handles death specifically on the player entity.
/// </summary>
public class PlayerDeathHandler : MonoBehaviour, IDeathHandler
{
    private Animation deathAnimation = null;
    

    private void Awake()
    {
        
    }

    public void HandleDeath()
    {
       if(deathAnimation != null)
        { deathAnimation.Play(); }

    }
}
