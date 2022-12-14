using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Waits a given amount of time before setting active to true on given object.
/// </summary>
public class WaitToAwake : MonoBehaviour
{
    [SerializeField] private List<GameObject> objectsToWake = null;
    [Tooltip("How long in seconds it should take before the object is set active.")]
    [SerializeField] private float timeToAwake = 5f;

    private void Awake()
    {
        float curTimeToAwake = timeToAwake;
        while(curTimeToAwake >= 0)
        {
            curTimeToAwake -= Time.deltaTime;
        }
        foreach(GameObject go in objectsToWake)
        {
            go.SetActive(true);
        }
    }
}
