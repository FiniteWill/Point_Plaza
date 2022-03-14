using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] private GameObject item = null;
    [SerializeField] private string[] interactibleTags = null;
    [SerializeField] private Item itemDefinition = Item.Debug; 

    private void Awake()
    {
        Assert.IsNotNull($"{this.name} does not have a {nameof(item)} but requires one.");
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null)
        {
            foreach (string tag in interactibleTags)
            {
                if (collision.CompareTag(tag)) { HandleEffect(); }
            }
        }
    }

    private void HandleEffect()
    {
        gameObject.SetActive(false);

        // TODO: Define behaviors for item pickup
        switch(itemDefinition)
        {
            case Item.Debug:
                Debug.Log($"{this.name} was successfully collected.");
                break;
            case Item.SmallHealthBoost:
                break;
            default:
                break;
        }
    }
}
