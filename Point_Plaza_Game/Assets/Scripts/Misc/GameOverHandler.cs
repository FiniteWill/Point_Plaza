using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Handler for GameOver when conditions are met in a game.
/// </summary>
public class GameOverHandler : MonoBehaviour
{
    [SerializeField] private List<Game> games = null;
    private void Awake()
    {
        Assert.IsNotNull(games, $"{name} does not have a serialized {games.GetType()} but requires one.");
    }
}
