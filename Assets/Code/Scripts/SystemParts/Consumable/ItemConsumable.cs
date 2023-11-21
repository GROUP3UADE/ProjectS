using UnityEngine;

[CreateAssetMenu(fileName = "new Item", menuName = "Scriptables/Item/Consumable", order = 0)]
public class ItemConsumable : ItemSO
{
    [SerializeField] private ItemAction itemAction;

    private void OnEnable()
    {
        itemCategories = ItemCategories.Consumable;
    }

    public override void ItemAction()
    {
        if (GameManager.Instance.PlayerInventory.CheckItemSO(this, 1))
        {
            itemAction.Execute();
            GameManager.Instance.PlayerInventory.RemoveItemSO(this, 1);
        }
        else
        {
            Debug.Log("Missing the required item");
        }
    }
}