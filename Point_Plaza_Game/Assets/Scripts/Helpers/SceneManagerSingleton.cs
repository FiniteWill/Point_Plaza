using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerSingleton : MonoBehaviour
{
    public static SceneManagerSingleton Instance { get; private set; }
   
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
        if (scene != null) { SceneManager.LoadSceneAsync(scene.name); }
    }

    public void LoadScene(string sceneName)
    {
        if (SceneManager.GetSceneByName(sceneName) != null) { SceneManager.LoadScene(sceneName); }
    }
}