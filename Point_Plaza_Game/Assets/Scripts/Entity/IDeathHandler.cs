using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interface for all entities that need to handle death.
/// </summary>
public interface IDeathHandler
{
    public void HandleDeath();
}
