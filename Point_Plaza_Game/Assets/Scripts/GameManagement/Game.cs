using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Holds the data related to a game.
/// </summary>
public class Game : MonoBehaviour
{
    [SerializeField] private Scene gameScene;
    [SerializeField] private IPlayer player;

    private bool hasStarted = false;
    private bool isPaused = false;

    public event Action onGameStart;
    public event Action onGamePause;
    public event Action onGameOver;

    public void HandleGameStart()
    {

    }
    public void HandleGamePause()
    {

    }
    public void HandleGameOver()
    {

    }



}
