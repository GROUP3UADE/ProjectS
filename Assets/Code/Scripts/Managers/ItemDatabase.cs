using System.Collections.Generic;
using UnityEngine;

public enum ItemCategories
{
    All,
    Equipment,
    Consumable,
    Material,
    Key,
}

public class ItemDatabase : MonoBehaviour
{
    [SerializeField] private List<ItemSO> itemsSODatabase = new();

    public ItemSO GetItemSO(ItemSO itemSO)
    {
        return itemsSODatabase.Find(item => item == itemSO);
    }

    public ItemSO GetItemSO(string identifier)
    {
        return itemsSODatabase.Find(item => item.Identifier == identifier);
    }
}