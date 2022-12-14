using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthGameOverMonitor : MonoBehaviour
{
    [SerializeField] private Health playerHealth = null;
    [SerializeField] private AudioSource gameOverSFX = null;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth.onHealthReachedZero += HandleGameOver;
    }


    private void HandleGameOver()
    {
        AudioManagerSingleton.Instance.PauseAllSFX();
        AudioManagerSingleton.Instance.PauseAllTracks();
        AudioManagerSingleton.Instance.PlayAudio(gameOverSFX);
        //gameOverSFX.Play();
        float temp = gameOverSFX.clip.length;
        while(temp > 0 )
        {
            temp -= Time.deltaTime;
        }
        SceneManagerSingleton.Instance.LoadScene("MainMenu");
    }
}
