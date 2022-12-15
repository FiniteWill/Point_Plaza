using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_GameData : MonoBehaviour
{
    [SerializeField] private string gameTitle = "Lava Climb";
    [SerializeField] [Min(0)] private int gameIndex = 0;

    private GameData gameData;

    // Start is called before the first frame update
    void Start()
    {
        gameIndex = Mathf.Min(GameManagerSingleton.Instance.Games.Count, gameIndex);
        gameData = GameManagerSingleton.Instance.Games[gameIndex].Data;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
