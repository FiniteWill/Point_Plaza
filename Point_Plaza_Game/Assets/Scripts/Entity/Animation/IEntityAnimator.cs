using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interface for any entity that has animations. Contains function for handling spawning and handling death.
/// </summary>
public interface IEntityAnimator
{
    public void HandleSpawn();
    public void HandleDeath();
}
