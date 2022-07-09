using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class TimeLimitHandler : MonoBehaviour
{
    private Timer timer = null;
    private Game game = null;

    // Start is called before the first frame update
    void Start()
    {
        game = GetComponentInParent<Game>();
        Assert.IsNotNull(game, $"{name} could not find an attached {typeof(Game)} but requires one.");
        timer = FindObjectOfType<Timer>();
        Assert.IsNotNull(timer, $"{name} could not find a {typeof(Timer)} but requires one.");
        timer.onTimeLimitReached += HandleTimeLimitReached;
    }

    private void OnDestroy()
    {
        if (timer != null)
        {
            timer.onTimeLimitReached -= HandleTimeLimitReached;
        }
    }

    private void HandleTimeLimitReached()
    {
        game.HandleGameOver();
    }
}
