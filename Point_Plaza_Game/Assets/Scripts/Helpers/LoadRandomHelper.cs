using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

public class LoadRandomHelper : MonoBehaviour
{
    [SerializeField] private List<string> scenes = null;

    private void Awake()
    {
        Assert.IsNotNull(scenes, $"{name} does not have any scenese to load.");
    }

    public void LoadScene()
    {
        //SceneManager.LoadSceneAsync(Random.Range(0, scenes.Count - 1));
        if (scenes.Count > 0)
        {
            SceneManagerSingleton.Instance.LoadScene(scenes[Random.Range(0, scenes.Count - 1)]);
        }
    }
}
