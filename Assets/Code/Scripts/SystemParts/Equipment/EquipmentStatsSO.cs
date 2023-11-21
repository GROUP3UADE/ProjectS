using System;
using UnityEngine;

[CreateAssetMenu(fileName = "new Equipment Stats", menuName = "Scriptables/Stats/Equipment", order = 0)]
public class EquipmentStatsSO : StatsSO
{
    [SerializeField] private EquipmentSlots equipSlot;
    public bool IsEquipped { get; private set; }

    [SerializeField] private int armor;
    [SerializeField] private int strength;

    public EquipmentSlots EquipSlot => equipSlot;
    public int Armor => armor;
    public int Strength => strength;

    private void OnEnable()
    {
        IsEquipped = false;
    }

    public void Equip(bool state)
    {
        IsEquipped = state;
    }
}