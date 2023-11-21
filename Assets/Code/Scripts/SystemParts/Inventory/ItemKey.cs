using UnityEngine;

[CreateAssetMenu(fileName = "new Item", menuName = "Scriptables/Item/Key", order = 0)]
public class ItemKey : ItemSO
{
    private void OnEnable()
    {
        itemCategories = ItemCategories.Key;
    }
}