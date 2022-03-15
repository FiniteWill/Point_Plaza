using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private Text display = null;
    private float timer;

    private void Awake()
    {
        Assert.IsNotNull(display, $"{this.name} does not have a {nameof(display)} but requires one.");
    }

    private void Update()
    {
        timer += Time.deltaTime;
        int minutes = Mathf.FloorToInt(timer / 60F);
        int seconds = Mathf.FloorToInt(timer % 60F);
        display.text = "TIME " + minutes.ToString("00") + ":" + seconds.ToString("00");
    }
}
