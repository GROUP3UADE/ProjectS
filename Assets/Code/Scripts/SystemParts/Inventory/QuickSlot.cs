using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuickSlot : MonoBehaviour
{
    [SerializeField] private KeyCode input;
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI amount;

    private ItemSO _savedItem;
    private PlayerInventory _inventory;

    public ItemSO DEBUGITEM;

    private void Start()
    {
        _inventory = GetComponent<PlayerInventory>();
    }

    public void SaveItem(ItemSO item)
    {
        print("Save item quick");
        _savedItem = item;
        UpdateVisuals();
    }

    private void Update()
    {
        if (Input.GetKeyDown(input))
        {
            UseQuickSlot();
        }
    }

    private void UseQuickSlot()
    {
        if (_savedItem != null && _inventory.CheckItemSO(_savedItem, 1))
        {
            _savedItem.ItemAction();
            UpdateVisuals();
        }
    }

    private void UpdateVisuals()
    {
        var curr = _inventory.CheckItemSO(_savedItem);
        if (curr > 0)
        {
            amount.text = curr.ToString();
            icon.color = Color.white;
            icon.sprite = _savedItem.Icon;
        }
        else
        {
            amount.text = "";
            icon.color = Color.clear;
            _savedItem = null;
        }
    }

    [ContextMenu("DEBUG")]
    private void DEBUG()
    {
        SaveItem(DEBUGITEM);
    }
}