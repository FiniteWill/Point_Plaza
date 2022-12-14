using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

/// <summary>
/// Holds the data related to a game.
/// </summary>
public class Game : MonoBehaviour, ISavable
{
    [SerializeField] private bool isDebugging = false;
    [SerializeField] private string menuScene = "MainMenu";
    [SerializeField] private AudioSource gameTrack = null;
    public Scene MenuScene => SceneManager.GetSceneByName(menuScene);

    [SerializeField] private List<string> gameScenes = null;
    public List<string> GetScenes => gameScenes;
    [SerializeField] private string gameScene = "SampleScene";
    public Scene GameScene => SceneManager.GetSceneByName(gameScene);
    [SerializeField] private IPlayer player;
     private Vector3 startPos =new  Vector3(-14.6000004f, -29.1000004f, 0f);
    [SerializeField] private string gameID;
    public string ID => gameID;
    [SerializeField] [Min(0)] private int gameIndex = 0;

    private GameData data;
    public GameData Data => data;
    private Achievement[] achievements;

    private int hiScore = 0;
    private int curScore = 0;
    public int GetScore() { return curScore; }
    public void ChangeScore(int change) 
    { 
        curScore += change;
    }
    public void AddToScore(int add)
    {
        curScore += Mathf.Max(0, add);
    }
    private int startingLives = 3;
    private int curLives = 0;
    private bool hasStarted = false;
    private bool isPaused = false;

    public event Action onGameStart;
    public event Action<bool> onGamePause;
    public event Action onGameOver;

    private void Awake()
    {
        // Local initialization
        SaveData saveData = new SaveData();
        data = saveData.gameData[gameIndex];
        Assert.IsNotNull(gameTrack);
    }

    public void HandleGameStart()
    {
        if (isDebugging) { Debug.Log($"Game {gameID} loaded."); }
        if (startPos != null)
        {
            (player as MonoBehaviour).transform.position = startPos;
        }
        onGameStart?.Invoke();
        curScore = 0;
        curLives = startingLives;
        hasStarted = true;
        SceneManagerSingleton.Instance.LoadScene(gameScene);
        AudioManagerSingleton.Instance.PlayAudio(gameTrack);
    }
    public void HandleGamePause(bool cond)
    {
        if (cond)
        {
            gameTrack.Pause();
        }
        else
        {
            gameTrack.Play();
        }
        isPaused = cond;
        onGamePause?.Invoke(isPaused);

    }
    public void HandleGameOver()
    {
        onGameOver?.Invoke();
        hasStarted = false;
        Save();
        SceneManagerSingleton.Instance.LoadScene(menuScene);
    }

    private void Save()
    {
        SaveData sd = new SaveData();
        SaveData(sd);
        // Write data to save file
        if (FileManager.WriteToFile(FileManager.SAVE_DATA_FILE_NAME, sd.ToJSon()))
        {
            Debug.Log($"Saved {name} stats successfully.");
        }
    }

    private void Load()
    {
        if(FileManager.LoadFromFile(FileManager.SAVE_DATA_FILE_NAME, out var json))
        {
            SaveData sd = new SaveData();
            sd.LoadFromJson(json);

            LoadData(sd);
            Debug.Log($"Load completed for {name}");
        }
    }

    public void SaveData(SaveData data)
    {
        data.gameData[gameIndex] = new GameData(gameID, new Achievement[0], Mathf.Max(curScore,hiScore));
    }

    public void LoadData(SaveData data)
    {
        hiScore = data.gameData[gameIndex].hiScore;
    }
}

public struct GameData
{
    public string gameTitle;
    public int hiScore;
    public Achievement[] achievements;

    public GameData(string name, Achievement[] achievements)
    {
        gameTitle = name;
        hiScore = 0;
        this.achievements = achievements;
    }
    public GameData(string name, Achievement[] achievements, int score)
    {
        gameTitle = name;
        hiScore = score;
        this.achievements = achievements;
    }
}