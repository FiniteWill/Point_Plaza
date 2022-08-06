using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[Serializable]
public enum ItemAction { CHANGE_HEALTH, CHANGE_SPEED, CHANGE_POINTS, };

public class ItemPickup : MonoBehaviour
{
    [SerializeField] private bool isDebugging = false;
    [SerializeField] private GameObject itemPickup = null;
    [SerializeField] private string[] interactibleTags = null;
    [SerializeField] private int itemID = 0;
    private ItemDefinitions.Item item;
    //[SerializeField] private ItemDefinitions.Item itemType = ItemDefinitions.Item.Debug; 

    [SerializeField] private Tuple<ItemAction, int> actionDefinition = new Tuple<ItemAction, int>(ItemAction.CHANGE_HEALTH, 1);
     
    private void Awake()
    {
        Assert.IsNotNull($"{name} does not have a {nameof(itemPickup)} but requires one.");
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

        // TODO: Define behaviors for item pickup
        if (isDebugging) { Debug.Log($"{name} was sucessfully collected."); }
        switch(actionDefinition.Item1)
        {
            case ItemAction.CHANGE_HEALTH:
                Health tempHealth = collision.GetComponentInChildren<Health>();
                if(ValidateEffect(collision, ItemAction.CHANGE_HEALTH, tempHealth))
                {
                    tempHealth.ChangeHealth(actionDefinition.Item2);
                }
                break;
            case ItemAction.CHANGE_POINTS:
                Game game = GameManagerSingleton.Instance.GetGameForCurScene();
                if(ValidateEffect(collision, ItemAction.CHANGE_POINTS, game))
                {
                    game.ChangeScore(actionDefinition.Item2);
                }
                break;
            case ItemAction.CHANGE_SPEED:
                //if(ValidateEffect(collision, Action.CHANGE_SPEED, Pla))
                break;
            default:
                Debug.LogError($"{name} was given invalid value {actionDefinition.Item1} and cannot handle the effect.");
                break;
        }
    }

    private bool ValidateEffect(Collider2D collision, ItemAction action, Component component)
    {
        Assert.IsNotNull(collision, $"{name} cannot perform action {action} because {collision} was null.");
        Assert.IsNotNull(component, $"{name} cannot perform action {action} because {collision} is missing a {component}");
        return component != null;
    }
}
