using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles health and damage as well as any other stats directly related to the PlatformerPlayer.
/// </summary>
public class PlatformerPlayer_Stats : MonoBehaviour
{
    // Score
    private int curScore;
    public int score { get => curScore; set => curScore = value; }
    [SerializeField] private Health health = null;
}
