using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Functionality for a button that quits the game 
/// </summary>
[RequireComponent(typeof(Button))]
public class QuitButton : MonoBehaviour
{
    private Button quitButton = null;

    private void Awake()
    {
        quitButton = GetComponent<Button>();
        quitButton.onClick.AddListener(Quit);
    }

    private void OnEnable()
    {
        if (quitButton != null)
        {
            quitButton.onClick.AddListener(Quit);
        }
    }
    private void OnDisable()
    {
        if (quitButton != null)
        {
            quitButton.onClick.RemoveAllListeners();
        }
    }

    private void Quit()
    {
#if UNITY_EDITOR
        Application.Quit();
        EditorApplication.isPlaying = false;
#else
    Application.Quit();
    
#endif
    }
}
