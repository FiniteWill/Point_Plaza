using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[Serializable]
public enum ItemAction { NONE, CHANGE_HEALTH, CHANGE_SPEED, CHANGE_JUMP, CHANGE_POINTS, };
public enum ItemEffectType { NONE, SPEED, JUMP, POINTS };

public class ItemPickup : MonoBehaviour
{
    [SerializeField] private bool isDebugging = false;
    [SerializeField] private GameObject itemPickup = null;
    [SerializeField] private string[] interactibleTags = null;
    [SerializeField] private AudioSource pickupSFX = null;
    [SerializeField] private int itemID = 0;
    [SerializeField] private string itemName = "";
    private ItemDefinitions.Item item;
    [SerializeField] private bool hasCollectionAnim = false;
    [SerializeField] private CollectibleAnimation collectibleAnim;

    // ItemActions are being stored in ItemDefinitions and are cannot be set per item pickup because Unity serialization doesn't allow enums, structs, or tuples by default...

    //[SerializeField] private ItemDefinitions.Item itemType = ItemDefinitions.Item.Debug; 
    [SerializeField] private Tuple<ItemAction, int> actionDefinition = new Tuple<ItemAction, int>(ItemAction.CHANGE_HEALTH, 1);
     
    private void Awake()
    {
        Assert.IsNotNull($"{name} does not have a {nameof(itemPickup)} but requires one.");
        if (itemName != "")
        {
            item = ItemDefinitions.GetItem(name);
        }
        else
        {
            item = ItemDefinitions.GetItem(itemID);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null)
        {
            foreach (string tag in interactibleTags)
            {
                if (pickupSFX != null) { pickupSFX.Play(); }
                if (collision.CompareTag(tag)) { HandleEffect(collision); }
                float temp = pickupSFX.clip.length;
                while (temp > 0)
                {
                    temp -= Time.deltaTime;
                    Destroy(this);
                }
            }
        }
    }

    private void HandleEffect(Collider2D collision)
    {
        gameObject.SetActive(false);

        // TODO: Define behaviors for item pickup
        if (isDebugging) { Debug.Log($"{name} was sucessfully collected."); }
        switch(item.Action())
        {
            case ItemAction.CHANGE_HEALTH:
                Health tempHealth = collision.GetComponentInChildren<Health>();
                if(ValidateEffect(collision, ItemAction.CHANGE_HEALTH, tempHealth))
                {
                    tempHealth.ChangeHealth(item.EffectValue());
                }
                break;
            case ItemAction.CHANGE_POINTS:
                Game game = GameManagerSingleton.Instance.GetGameForCurScene();
                if(ValidateEffect(collision, ItemAction.CHANGE_POINTS, game))
                {
                    game.ChangeScore(item.EffectValue());
                }
                break;
            case ItemAction.CHANGE_SPEED:
                PlatformerPlayer_Movement tempMovement = collision.GetComponentInChildren<PlatformerPlayer_Movement>();
                if(ValidateEffect(collision, ItemAction.CHANGE_SPEED, tempMovement))
                {
                    // TODO: CHANGE TO ALLOW FOR MULTIPLICATIVE AND ADDITIVE MODIFIERS
                    tempMovement.SpeedBoostMult(item.EffectValue(), 15f);
                }
                break;
            case ItemAction.NONE:
                break;
            default:
                Debug.LogError($"{name} was given invalid value {item.Action()} and cannot handle the effect.");
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
