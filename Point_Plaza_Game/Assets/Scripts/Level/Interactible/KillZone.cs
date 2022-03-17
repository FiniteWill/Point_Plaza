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

    public bool isPaused = false;

    private void Awake()
    {
        Assert.IsNotNull(interactibleTags, $"{this.name} does not have any tags specified for interaction.");
        collider = GetComponent<Collider2D>();
        collider.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isPaused)
        {
            Debug.Log($"{other.name} has triggered {this.name}");

            foreach (string tag in interactibleTags)
            {
                if (other.CompareTag(tag))
                {
                    // Affect health
                    Health entityHealth = other.GetComponent<Health>();
                    if (entityHealth != null)
                    { entityHealth.health = 0; }
                    else { Debug.Log($"{other.name} does not have an attached {nameof(entityHealth)}"); }
                }
            }
        }
    }
}
