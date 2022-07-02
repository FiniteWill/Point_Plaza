using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles health and damage as well as any other stats directly related to the PlatformerPlayer.
/// </summary>
public class PlatformerPlayer_Stats : MonoBehaviour
{
    // Score
    public int curScore { get; private set; }
    //public ItemDefinitions.EquipItem[] equipmentSlots = new ItemDefinitions.EquipItem[2] {ItemDefinitions.s_noArmor, ItemDefinitions.s_noWeapon};
    public List<ItemDefinitions.Item> inventory = null;
    [SerializeField] private Health health = null;

}
