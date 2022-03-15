using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(Collider2D))]
public class KillZone : MonoBehaviour
{
    [SerializeField] private bool isDebugging = false;
    [SerializeField] private List<string> interactibleTags = null;
    private Collider2D collider;

    private void Awake()
    {
        Assert.IsNotNull(interactibleTags, $"{this.name} does not have any tags specified for interaction.");
        collider = GetComponent<Collider2D>();
        collider.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        foreach(string tag in interactibleTags)
        {
            if(other.CompareTag(tag))
            {
                // Affect health
            }
        }
    }
}
