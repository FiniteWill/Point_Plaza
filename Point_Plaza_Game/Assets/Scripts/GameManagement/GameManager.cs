using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<Game> games = null;


    private void Awake()
    {
        Assert.IsNotNull(games, $"{name} does not have a serialized {games.GetType()} but requires one.");
    }

    
}
