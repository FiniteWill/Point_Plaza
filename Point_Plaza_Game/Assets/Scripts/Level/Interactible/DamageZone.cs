using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(Collider2D))]
public class DamageZone : MonoBehaviour
{
    [SerializeField] private bool isDebugging = false;
    [SerializeField] private bool isKillZone = false;
    [SerializeField] private int damageToDeal = 1;
    [SerializeField] private float damageDelay = 3f;
    [SerializeField] private List<string> interactibleTags = null;

    private Collider2D zoneCollider;
    private List<Health> damagedEntities = new List<Health>();

    public bool isPaused = false;

    private void Awake()
    {
        Assert.IsNotNull(interactibleTags, $"{name} does not have any tags specified for interaction.");
        zoneCollider = GetComponent<Collider2D>();
        zoneCollider.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isPaused)
        {
            if (isDebugging) { Debug.Log($"{other.name} has triggered {name}"); }

            foreach (string tag in interactibleTags)
            {
                if (other.CompareTag(tag))
                {
                    // Affect health
                    Health entityHealth = other.GetComponent<Health>();
                    if (entityHealth != null && !damagedEntities.Contains(entityHealth))
                    {
                        if (isKillZone) { entityHealth.SetHealth(0); }
                        else 
                        { 
                            entityHealth.TakeDamage(damageToDeal);
                            StartCoroutine(Delay(damageDelay, entityHealth));
                        }
                    }
                    else if(isDebugging){ Debug.Log($"{other.name} does not have an attached {nameof(entityHealth)}"); }
                }
            }
        }
    }

    private IEnumerator Delay(float delay, Health entityHealth)
    {
        if (!damagedEntities.Contains(entityHealth))
        {
            damagedEntities.Add(entityHealth);
            yield return new WaitForSeconds(delay);
            damagedEntities.Remove(entityHealth);
        }
        yield return null;
    }
}
