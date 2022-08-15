using UnityEngine;

public class ExitPortal : MonoBehaviour, IPausable
{
    [SerializeField] private bool endGamePortal = false;

    private Game game = null;
    
    public bool IsPaused { get; private set; } = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!IsPaused)
        {
            if(collision.CompareTag("Player"))
            {
                if (endGamePortal)
                {
                    game.HandleGameOver();
                }
                else
                {
                    
                }
            }
        }
    }

    public void Pause(bool toggle)
    {
        IsPaused = toggle;
    }
}
