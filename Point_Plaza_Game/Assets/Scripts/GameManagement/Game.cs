using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Holds the data related to a game.
/// </summary>
public class Game : MonoBehaviour
{
    [SerializeField] private string menuScene = "MainMenu";
    public Scene MenuScene => SceneManager.GetSceneByName(menuScene);
    [SerializeField] private string gameScene = "SampleScene";
    public Scene GameScene => SceneManager.GetSceneByName(gameScene);
    [SerializeField] private IPlayer player;
    [SerializeField] private Transform startPos;
    [SerializeField] private string gameID;
    public string ID => gameID;

    private int hiScore = 0;
    private int curScore = 0;
    public void AddToScore(int add)
    {
        curScore = Mathf.Max(0, add);
    }
    private int startingLives = 3;
    private int curLives = 0;
    private bool hasStarted = false;
    private bool isPaused = false;

    public event Action onGameStart;
    public event Action<bool> onGamePause;
    public event Action onGameOver;

    public void HandleGameStart()
    {
        if (startPos != null)
        {
            (player as MonoBehaviour).transform.position = startPos.position;
        }
        onGameStart?.Invoke();
        curScore = 0;
        curLives = startingLives;
        hasStarted = true;
        SceneManagerSingleton.Instance.LoadScene(gameScene);
    }
    public void HandleGamePause(bool cond)
    {
        isPaused = cond;
        onGamePause?.Invoke(isPaused);

    }
    public void HandleGameOver()
    {
        onGameOver?.Invoke();
        hiScore = Mathf.Max(curScore, hiScore);
        hasStarted = false;
        SceneManagerSingleton.Instance.LoadScene(menuScene);
    }
}
