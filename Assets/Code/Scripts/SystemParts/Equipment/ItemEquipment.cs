using UnityEngine;

[CreateAssetMenu(fileName = "new Item", menuName = "Scriptables/Item/Equipment", order = 0)]
public class ItemEquipment : ItemSO
{
    private void OnEnable()
    {
        itemCategories = ItemCategories.Equipment;
    }
}