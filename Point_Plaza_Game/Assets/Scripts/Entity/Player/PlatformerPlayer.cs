using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Script that handles Player controls for a character in one of the platformer games.
/// </summary>
public class PlatformerPlayer : MonoBehaviour
{
    // Debug mode
    [SerializeField] private bool isDebugging = false;
    // Serialized Components
    [SerializeField] private PlatformerPlayer_Animator playerAnimator = null;
    [SerializeField] private PlatformerPlayer_Movement playerMovement = null;
    [SerializeField] private PlatformerPlayer_Stats playerStats = null;

    private bool isPaused = false;

    private void Awake()
    {
        Assert.IsNotNull(playerAnimator, $"{this.name} does not have a {nameof(playerAnimator)} assigned but requires one.");
        Assert.IsNotNull(playerMovement, $"{this.name} does not have a {nameof(playerMovement)} assigned but requires one.");
        Assert.IsNotNull(playerStats, $"{this.name} does not have a {nameof(playerStats)} assigned but requires one.");
    }

    private void Update()
    {
        
    }

    /// <summary>
    /// Checks the player's current status and sets the animation state on the animator accordingly.
    /// </summary>
    private void SetAnimationState()
    {
        // Check what movement the player is performing 

    }

}
