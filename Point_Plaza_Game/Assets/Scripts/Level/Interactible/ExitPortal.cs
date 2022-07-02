using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPortal : MonoBehaviour, IPausable
{
    private Game game = null;
    public bool IsPaused { get; private set; } = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!IsPaused)
        {
            if(collision.CompareTag("Player"))
            {
                game.HandleGameOver();
            }
        }
    }

    public void Pause(bool toggle)
    {
        IsPaused = toggle;
    }
}
