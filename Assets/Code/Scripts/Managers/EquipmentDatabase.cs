using System.Collections.Generic;
using UnityEngine;

public enum EquipmentSlots
{
    Head,
    Chest,
    Hands,
    Legs,
    Feet,
    //Arma
    MainHand
}

public class EquipmentDatabase : MonoBehaviour
{
    public Dictionary<EquipmentSlots, ItemSO> EquippedItems { get; private set; }

    public int Armor { get; private set; }
    public int Strength { get; private set; }

    private void Awake()
    {
        EquippedItems =
            new()
            {
                { EquipmentSlots.Head, null },
                { EquipmentSlots.Chest, null },
                { EquipmentSlots.Hands, null },
                { EquipmentSlots.Legs, null },
                { EquipmentSlots.Feet, null },
                { EquipmentSlots.MainHand, null }
            };
    }


    public void EquipSlotUpdate(EquipmentSlots slot, ItemSO item)
    {
        EquippedItems[slot] = item;
        CalculateAttributes();
    }

    public void PutEquipmentOn(ItemSO item)
    {
        if (item.ItemCategories == ItemCategories.Equipment)
        {
            var stats = (EquipmentStatsSO)item.Stats;
            var equipSlot = stats.EquipSlot;
            RemoveEquipment(equipSlot);
            stats.Equip(true);
            EquipSlotUpdate(equipSlot, item);
        }
    }

    public void RemoveEquipment(EquipmentSlots slot)
    {
        if (EquippedItems[slot] != null)
        {
            var stats = (EquipmentStatsSO)EquippedItems[slot].Stats;
            stats.Equip(false);
            EquipSlotUpdate(slot, null);
        }
    }

    public void CalculateAttributes()
    {
        CalculateArmor();
        CalculateStrength();
    }

    public void CalculateArmor()
    {
        Armor = 0;
        foreach (var slot in EquippedItems)
        {
            if (slot.Value != null)
            {
                var itemStats = (EquipmentStatsSO)slot.Value.Stats;
                Armor += itemStats.Armor;
            }
        }
    }

    public void CalculateStrength()
    {
        Strength = 0;
        foreach (var slot in EquippedItems)
        {
            if (slot.Value != null)
            {
                var itemStats = (EquipmentStatsSO)slot.Value.Stats;
                Strength += itemStats.Strength;
            }
        }
    }
}