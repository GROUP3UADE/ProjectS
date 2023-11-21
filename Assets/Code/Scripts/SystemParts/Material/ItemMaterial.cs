using UnityEngine;

[CreateAssetMenu(fileName = "new Item", menuName = "Scriptables/Item/Material", order = 0)]
public class ItemMaterial : ItemSO
{
    private void OnEnable()
    {
        itemCategories = ItemCategories.Material;
    }
}