using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

public class SceneManagerSingleton : MonoBehaviour
{
    public static SceneManagerSingleton Instance { get; private set; }
    public Scene mainScene { get; private set; }
    public string mainSceneName { get; set; }
    public Scene persistentScene { get; private set; }
    public event Action onSceneChanged;
    public event Action<Scene> onSceneChangedTo;
    public event Action<Scene, Scene> onSceneChangedFromTo;
   
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
            mainScene = SceneManager.GetSceneByName("MainMenu");
            persistentScene = SceneManager.GetSceneByName("Persistent");
        }
    }

    public void LoadScene(Scene scene)
    {
        Debug.LogError($"LOADING {scene.name}");
        onSceneChanged?.Invoke();
        onSceneChangedTo?.Invoke(scene);
        onSceneChangedFromTo?.Invoke(SceneManager.GetActiveScene(), scene);
        if (scene != null) 
        {
            Scene curScene = SceneManager.GetActiveScene();
            SceneManager.LoadSceneAsync(scene.name, LoadSceneMode.Additive);
            SceneManager.UnloadSceneAsync(curScene.name);
        }
    }

    public void LoadScene(string sceneName)
    {
        onSceneChanged?.Invoke();
        onSceneChangedTo?.Invoke(SceneManager.GetSceneByName(sceneName));
        onSceneChangedFromTo?.Invoke(SceneManager.GetActiveScene(), SceneManager.GetSceneByName(sceneName));
        if (SceneManager.GetSceneByName(sceneName) != null) { SceneManager.LoadScene(sceneName); }
    }
}