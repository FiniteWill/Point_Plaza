using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private Text display = null;
    private int seconds = 0;
    private int minutes = 0;
    private WaitForSecondsRealtime oneSecond = new WaitForSecondsRealtime(1f);

    private void Awake()
    {
        Assert.IsNotNull(display, $"{this.name} does not have a {nameof(display)} but requires one.");
        StartCoroutine(Tick());
    }


    IEnumerator Tick()
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
}
