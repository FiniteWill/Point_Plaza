using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Continusously moves object in given direction. Made for endless platforming (instead of MoveObjectToPoint).
/// </summary>
public class MoveObject : MonoBehaviour
{
    [SerializeField] private bool isDebugging = false;
    [SerializeField] private Transform objectToMove = null;
    [SerializeField] private Vector3 directionToMove = Vector3.zero;
    [SerializeField] private bool moving = true;

    private void Awake()
    {
        Assert.IsNotNull(objectToMove, $"{this.name} does not have an object to move {nameof(objectToMove)}");
    }

    private void Update()
    {
        if (moving)
        {
            if (isDebugging)
            { Debug.Log("Move"); }
            objectToMove.position += directionToMove;
        }

    }
}
