using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemDefinitions : MonoBehaviour
{
    // Health
    public const int SMALL_HEALTH_BOOST = 1;
    public const int MED_HEALTH_BOOST = 2;
    public const int BIG_HEALTH_BOOST = 3;
    // Stat Modifiers
    public const int SMALL_SPEED_BOOST = 2;
    public const int MED_SPEED_BOOSTD = 5;
    public const int BIG_SPEED_BOOST = 10;
    // Equipment Vals
    public const int WEAK_EQUIP = 3;
    public const int MID_EQUIP = 3;
    public const int GOOD_EQUIP = 5;
    public const int POW_EQUIP = 7;

    public const int DEFAULT_ITEM_ID = 0;
    public const int NO_ARMOR_ID = 1;
    public const int NO_WEAPON_ID = 2;

    public static Item s_defaultItem = new Item(false, ItemType.HealthItem, 0, 0, "DEFAULT ITEM");
    public static Item s_noArmor = new Item(false, EquipmentSlot.Weapon, 0, 1, "NO ARMOR");
    public static Item s_noWeapon = new Item(false, EquipmentSlot.Weapon, 0, 2, "NO WEAPON");

    private static int index = 0;

    public static List<Item> totalInventoryList =
        new List<Item>()
        {
            new Item(true, ItemType.HealthItem, 0, index, "DEFAULT ITEM"),
            new Item(true, EquipmentSlot.Armor, 0, index, "NO ARMOR"),
            new Item(true, EquipmentSlot.Weapon, 0, index, "NO WEAPON"),
            new Item(true, ItemType.HealthItem, SMALL_HEALTH_BOOST, index, "Small Health Boost"),
            new Item(true, ItemType.HealthItem, MED_HEALTH_BOOST, index, "Medium Health Boost"),
            new Item(true, ItemType.StatItem, SMALL_SPEED_BOOST, index, "Small Speed Boost"),
            new Item(true, ItemType.StatItem, MED_SPEED_BOOSTD, index, "Medium Speed Boost"),
            new Item(true, EquipmentSlot.Armor, WEAK_EQUIP, index, "Rusty Helmet"),
            new Item(true, EquipmentSlot.Weapon, WEAK_EQUIP, index, "PeeWee Blaster"),
            new Item(true, EquipmentSlot.Weapon, POW_EQUIP, index, "Omega Cannon")
        };
    public static Item GetItem(int id)
    {
        foreach(Item item in totalInventoryList)
        {
            if(item == id) { return item; }
        }
        return new Item();
    }

    public enum EquipmentSlot
    {
        Weapon,
        Armor
    }
    public enum ItemType
    {
        HealthItem,
        StatItem,
        EquipItem
    }
    public struct Item
    {
        private readonly int id;
        public int ID() => id;
        private readonly string name;
        public string Name() => name;
        private readonly static ItemType type = ItemType.HealthItem;
        public ItemType Type() => type;
        private object itemObj;
        #region constructors
        public Item(ItemType type, int id, string name)
        {
            this.id = id;
            this.name = name;
            switch(type)
            {
                case ItemType.HealthItem:
                    itemObj = new HealthItem(SMALL_HEALTH_BOOST, id, name);
                    break;
                case ItemType.StatItem:
                    itemObj = new StatItem(SMALL_SPEED_BOOST, false, id, name);
                    break;
                case ItemType.EquipItem:
                    itemObj = new EquipmentItem(WEAK_EQUIP, id, name);
                    break;
                default:
                    itemObj = new HealthItem(SMALL_HEALTH_BOOST, id, name);
                    break;
            }
        }
        public Item(ItemType type, int val, int id, string name)
        {
            this.id = id;
            this.name = name;
            switch (type)
            {
                case ItemType.HealthItem:
                    itemObj = new HealthItem(val, id, name);
                    break;
                case ItemType.StatItem:
                    itemObj = new StatItem(val, false, id, name);
                    break;
                case ItemType.EquipItem:
                    itemObj = new EquipmentItem(val, id, name);
                    break;
                default:
                    itemObj = new HealthItem(val, id, name);
                    break;
            }
        }
        public Item(bool usingIndexer, ItemType type, int val, int id, string name)
        {
            this.id = id;
            id = usingIndexer ? id + 1: id;
            this.name = name;
            switch (type)
            {
                case ItemType.HealthItem:
                    itemObj = new HealthItem(val, id, name);
                    break;
                case ItemType.StatItem:
                    itemObj = new StatItem(val, false, id, name);
                    break;
                case ItemType.EquipItem:
                    itemObj = new EquipmentItem(val, id, name);
                    break;
                default:
                    itemObj = new HealthItem(val, id, name);
                    break;
            }
        }
        public Item(EquipmentSlot equipType, int val, int id, string name)
        {
            this.id = id;
            this.name = name;
            itemObj = new EquipmentItem(equipType, val, id, name);
        }
        public Item(bool usingIndexer, EquipmentSlot equipType, int val, int id, string name)
        {
            this.id = id;
            id = usingIndexer ? id + 1 : id;
            this.name = name;
            itemObj = new EquipmentItem(equipType, val, id, name);
        }
        #endregion constructors
        public static bool operator ==(Item i, int id) { return i.ID().Equals(id); }
        public static bool operator !=(Item i, int id) { return !i.ID().Equals(id); }
    }

    public struct HealthItem
    {
        private readonly int id;
        private readonly string name;
        private readonly int amountToHeal;
        public HealthItem(int heal)
        {
            amountToHeal = heal;
            id = 0;
            name = "DEFAULT HEALTH ITEM";
        }        
        public HealthItem(int heal, int id, string name)
        {
            amountToHeal = heal;
            this.id = id;
            this.name = name;
        }
    }
    
    public struct StatItem
    {
        private readonly int id;
        private readonly string name;
        private readonly int modifier;
        private readonly bool modIsMultiplicative;
        public StatItem(int mod)
        {
            modifier = mod;
            modIsMultiplicative = false;
            id = 0;
            name = "DEFAULT STAT ITEM";
        }
        public StatItem(int mod, bool mult)
        {
            modifier = mod;
            modIsMultiplicative = mult;
            id = 0;
            name = "DEFAULT STAT ITEM";
        }
        public StatItem(int id, string name)
        {
            modifier = 0;
            modIsMultiplicative = false;
            this.id = id;
            this.name = name;
        }
        public StatItem(int mod, int id, string name)
        {
            modifier = mod;
            modIsMultiplicative = false;
            this.id = id;
            this.name = name;
        }
        public StatItem(int mod, bool mult, int id, string name)
        {
            modifier = mod; ;
            modIsMultiplicative = mult;
            this.id = id;
            this.name = name;
        }
    }

    public struct EquipmentItem
    {
        private readonly int id;
        private readonly string name;
        private readonly int equipmentStat;
        private EquipmentSlot slotType;
        public EquipmentItem(EquipmentSlot slot)
        {
            slotType = slot;
            equipmentStat = 0;
            id = 0;
            name = "DEFAULT EQUIP ITEM";
        }
        public EquipmentItem(int stat)
        {
            slotType = EquipmentSlot.Armor;
            equipmentStat = stat;
            id = 0;
            name = "DEFAULT EQUIP ITEM";
        }
        public EquipmentItem(EquipmentSlot slot, int id, string name)
        {
            slotType = slot;
            equipmentStat = 0;
            this.id = id;
            this.name = name;
        }
        public EquipmentItem(int stat, int id, string name)
        {
            slotType = EquipmentSlot.Armor;
            equipmentStat = stat;
            this.id = id;
            this.name = name;
        }
        public EquipmentItem(EquipmentSlot slot, int stat, int id, string name)
        {
            slotType = slot;
            equipmentStat = stat;
            this.id = id;
            this.name = name;
        }
    }

}