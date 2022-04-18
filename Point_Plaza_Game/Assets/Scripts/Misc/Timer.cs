using System.Collections;
using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    /// <summary>
    /// Time limit for the current activity. (In seconds).
    /// </summary>
    [SerializeField] private int timeLimit = 60;
    [SerializeField] private Text display = null;

    private int seconds = 0;
    private int minutes = 0;
    private WaitForSecondsRealtime oneSecond = new WaitForSecondsRealtime(1f);

    public event Action onTimeLimitReached;

    private void Awake()
    {
        Assert.IsNotNull(display, $"{this.name} does not have a {nameof(display)} but requires one.");
        StartCoroutine(Tick());
    }


    private IEnumerator Tick()
    {
        if(seconds == 60)
        {
            seconds = 0;
            minutes++;
        }
        else
        { seconds++; }
        display.text = "TIME " +minutes.ToString("00") + ":" +seconds.ToString("00");
        yield return oneSecond;
        if(minutes < 60)
        { StartCoroutine(Tick()); }
    }

    private void HandleTimeLimitReached()
    {
        onTimeLimitReached?.Invoke();
    }
}
