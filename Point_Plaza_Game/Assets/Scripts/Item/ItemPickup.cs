using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;


public class ItemPickup : MonoBehaviour
{
    [SerializeField] private bool isDebugging = false;
    [SerializeField] private GameObject itemPickup = null;
    [SerializeField] private string[] interactibleTags = null;
    [SerializeField] private int itemID = 0;
    private ItemDefinitions.Item item;
    //[SerializeField] private ItemDefinitions.Item itemType = ItemDefinitions.Item.Debug; 

    private void Awake()
    {
        Assert.IsNotNull($"{this.name} does not have a {nameof(itemPickup)} but requires one.");
        item = ItemDefinitions.GetItem(itemID);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null)
        {
            foreach (string tag in interactibleTags)
            {
                if (collision.CompareTag(tag)) { HandleEffect(collision); }
            }
        }
    }

    private void HandleEffect(Collider2D collision)
    {
        gameObject.SetActive(false);
        Health tempHealth = collision.GetComponentInChildren<Health>();

        // TODO: Define behaviors for item pickup
        if (isDebugging) { Debug.Log($"{name} was sucessfully collected."); }
        //switch()
    }
}
