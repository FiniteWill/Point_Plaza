using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

public class GameManagerSingleton : MonoBehaviour
{
    [SerializeField] private List<Game> games = null;

    public static GameManagerSingleton Instance { get; private set; }
    public Action onLevelLoaded;


    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        Assert.IsNotNull(games, $"{name} does not have a serialized {games.GetType()} but requires one.");
        SceneManagerSingleton.Instance.onSceneChangedTo += HandleScene;
    }
    private void OnDestroy()
    {
        SceneManagerSingleton.Instance.onSceneChangedTo -= HandleScene;
    }

    public void HandleScene(Scene scene)
    {

    }
    public void ToggleGame(Game game)
    {
        foreach(Game g in games)
        {
            if(g == game)
            {
                g.HandleGameStart();
            }
        }
    }
    public Game GetGame(string gameName)
    {
        foreach(Game g in games)
        {
            if(g.ID == gameName)
            {
                return g;
            }
        }
        return null;
    }
    public Game GetGameForCurScene()
    {
        Scene curScene = SceneManager.GetActiveScene();
        foreach(Game g in games)
        {
            if(g.GameScene == curScene)
            {
                return g;
            }
        }
        return null;
    }
}
