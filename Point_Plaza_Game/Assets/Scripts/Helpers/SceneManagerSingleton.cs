using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerSingleton : MonoBehaviour
{
    public static SceneManagerSingleton Instance { get; private set; }
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
        }
    }

    public void LoadScene(Scene scene)
    {
        onSceneChanged?.Invoke();
        onSceneChangedTo?.Invoke(scene);
        onSceneChangedFromTo?.Invoke(SceneManager.GetActiveScene(), scene);
        if (scene != null) { SceneManager.LoadSceneAsync(scene.name); }
    }

    public void LoadScene(string sceneName)
    {
        onSceneChanged?.Invoke();
        onSceneChangedTo?.Invoke(SceneManager.GetSceneByName(sceneName));
        onSceneChangedFromTo?.Invoke(SceneManager.GetActiveScene(), SceneManager.GetSceneByName(sceneName));
        if (SceneManager.GetSceneByName(sceneName) != null) { SceneManager.LoadScene(sceneName); }
    }
}