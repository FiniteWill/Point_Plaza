using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class MoveObjectToPoint : MonoBehaviour
{
    [SerializeField] private bool isDebugging = false;
    [SerializeField] private Transform objectToMove = null;
    [SerializeField] private Transform objectToFollow = null;

    private void Awake()
    {
        Assert.IsNotNull(objectToFollow, $"{this.name} does not have a object to follow {nameof(objectToFollow)}");
        Assert.IsNotNull(objectToMove, $"{this.name} does not have an object to move {nameof(objectToMove)}");
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        if (isDebugging)
        { Debug.Log("Move"); }

        objectToMove.position = Vector3.MoveTowards(objectToMove.position, objectToFollow.position, 0.01f);
        yield return new WaitForEndOfFrame();
        if (objectToMove.position != objectToFollow.position)
        { StartCoroutine(Move()); }
        yield return null;
    }
}
