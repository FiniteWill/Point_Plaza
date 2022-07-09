using System.Collections;
using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    /// <summary>
    /// Maximum time allowed to be displayed on the timer. 60:59
    /// (60 min * 60 sec - 1 (since 60:60 would roll over to 1:00:00.)
    /// </summary>
    private const int MAX_TIME = 3599;
    private const int MIN_TIME = 10;

    private static readonly WaitForSecondsRealtime s_oneSecond = new WaitForSecondsRealtime(1f);

    /// <summary>
    /// Time limit for the current activity. (In seconds).
    /// </summary>
    [SerializeField, Range(MIN_TIME,MAX_TIME)] private int timeLimit = 60;
    [SerializeField] private int startTime = 0;
    [SerializeField] private Text display = null;
    [SerializeField] private bool countsDown = false;

    private long curTime = 0;
    private int timeRemaining = MIN_TIME;
    private int secondsPassed = 0;
    private int minutesPassed = 0;
    private int secondsLeft = 0;
    private int minutesLeft = 0;


    public event Action onTimeLimitReached;

    private void Awake()
    {
        Assert.IsNotNull(display, $"{this.name} does not have a {nameof(display)} but requires one.");
        timeRemaining = timeLimit;
        Tuple<int, int> formattedTime = FormatTime(startTime);
        SetTime(formattedTime.Item1, formattedTime.Item2);
        StartCoroutine(Tick());
    }
    /// <summary>
    /// Set and format the time text of the timer Text.
    /// </summary>
    /// <param name="min">Minute count.</param>
    /// <param name="sec">Second count.</param>
    private void SetTime(int min, int sec)
    {
        Mathf.Clamp(min, 0, 60);
        Mathf.Clamp(sec, 0, 60);
        minutesPassed = min;
        secondsPassed = sec;
        display.text = $"TIME {minutesPassed.ToString("00")}:{secondsPassed.ToString("00")}";
    }
    /// <summary>
    /// Resets and reformats the timer Text to be empty.
    /// </summary>
    private void ResetTimer()
    {
        curTime = 0;
        minutesPassed = 0;
        secondsPassed = 0;
        display.text = $"TIME 00:00";
        display.color = Color.white;
    }
    /// <summary>
    /// Changes a time given in seconds into a MINUTES : SECONDS format.
    /// </summary>
    /// <param name="time">Container for minutes and seconds of the MINUTES : SECONDS time format.</param>
    /// <returns></returns>
    private Tuple<int,int> FormatTime(int time)
    {
        int temp_mins = time / 60;
        int temp_secs = time % 60;
        return new Tuple<int,int>(temp_mins, temp_secs);
    }
    /// <summary>
    /// Resets the timer and announces that the time limit was reached.
    /// </summary>
    private void HandleTimeLimitReached()
    {
        ResetTimer();
        onTimeLimitReached?.Invoke();
    }

    private IEnumerator Tick()
    {
        curTime++;
        timeRemaining--;
        // Low timer check
        if(timeRemaining <= 10)
        {
            display.color = Color.red;
        }
        // Increment time passed rollover minute if necessary)
        if(secondsPassed == 60)
        {
            secondsPassed = 0;
            minutesPassed++;
        }
        else
        { secondsPassed++; }

        // Decrement time left
        if(secondsLeft == 0 && minutesLeft >= 1)
        {
            secondsLeft = 60;
        }
        else
        {
            secondsLeft--;
        }

        // Choose format of text
        if (countsDown)
        {
            display.text = $"TIME {minutesLeft.ToString("00")}:{secondsLeft.ToString("00")}";
        }
        else
        {
            display.text = $"TIME {minutesPassed.ToString("00")}:{secondsPassed.ToString("00")}";
        }
        yield return s_oneSecond;
        if(curTime < timeLimit)
        { StartCoroutine(Tick()); }
        else
        {
            HandleTimeLimitReached();
        }
    }
}
