using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// Destroys given items when an object's collider collides with given tags.
/// </summary>
public class DestroyOnContact : MonoBehaviour
{
    [SerializeField] private List<string> destroyableTags;

    private Collider2D contactCollider;

    private void Awake()
    {
        contactCollider = GetComponentInChildren<Collider2D>();
        Assert.IsNotNull(contactCollider);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Compare each destructible tag and destroy the object that collided if one matches.
        foreach (string curTag in destroyableTags)
        {
            if (collision.CompareTag(curTag))
            {
                Destroy(collision.gameObject);
                break;
            }
        }
                    }
}
