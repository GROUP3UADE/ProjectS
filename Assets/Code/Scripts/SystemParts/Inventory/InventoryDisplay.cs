using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDisplay : MonoBehaviour
{
    [SerializeField] private GameObject popupMenu;
    [SerializeField] private ScrollRect contentContainer;
    [SerializeField] private UI_ItemContainer itemPrefab;
    [SerializeField] private TextMeshProUGUI filterName;

    [Header("Info UI")] [Space(5)] [SerializeField]
    private GameObject infoContainer;

    [SerializeField] private TextMeshProUGUI itemIdentifier;
    [SerializeField] private TextMeshProUGUI itemCategory;
    [SerializeField] private TextMeshProUGUI itemDescription;
    [SerializeField] private Button interactionButton;
    [SerializeField] private Button quickSlotButton;


    private List<UI_ItemContainer> _itemsGenerated = new List<UI_ItemContainer>();
    private int _currentFilter;
    private int _lengthFilter;
    private QuickSlot quickSlot1;

    private void Awake()
    {
        _lengthFilter = Enum.GetValues(typeof(ItemCategories)).Length - 1;
        quickSlot1 = GetComponent<QuickSlot>();
    }

    private void GenerateInventoryItem(ItemSO itemSo, int itemAmount)
    {
        var go = Instantiate(itemPrefab, contentContainer.content.transform, true);
        if (!_itemsGenerated.Contains(go))
        {
            go.OnInteraction += ShowDetails;
            _itemsGenerated.Add(go);
        }

        if (itemSo.ItemCategories == ItemCategories.Key)
        {
            go.SetupButton(itemSo, "(!KEY!)");
        }
        else
        {
            // configura los datos que se ven en el boton
            go.SetupButton(itemSo, itemAmount);
        }

        //reset the item's scale -- this can get munged with UI prefabs
        go.transform.localScale = Vector2.one;
    }

    [ContextMenu("NextFilter")]
    public void NextFilter()
    {
        _currentFilter++;
        if (_currentFilter > _lengthFilter)
        {
            _currentFilter = 0;
        }

        UpdateFilterName();
        FilterInventory();
    }

    [ContextMenu("PrevFilter")]
    public void PreviousFilter()
    {
        _currentFilter--;
        if (_currentFilter < 0)
        {
            _currentFilter = _lengthFilter;
        }

        UpdateFilterName();
        FilterInventory();
    }

    private void FilterInventory()
    {
        var beforeClear = _itemsGenerated.Count;
        foreach (var item in _itemsGenerated)
        {
            item.OnInteraction -= ShowDetails;
            Destroy(item.gameObject);
        }

        _itemsGenerated.Clear();
        var inv = GameManager.Instance.PlayerInventory.PlayerInventoryDicSO;
        for (int i = inv.Count - 1; i >= 0; i--)
        {
            var kvP = inv.ElementAt(i);
            if ((ItemCategories)_currentFilter == ItemCategories.All)
            {
                GenerateInventoryItem(kvP.Key, kvP.Value);
            }
            else if (kvP.Key.ItemCategories == (ItemCategories)_currentFilter)
            {
                GenerateInventoryItem(kvP.Key, kvP.Value);
            }
        }

        if (_itemsGenerated.Count != beforeClear)
        {
            ClearDetails();
        }
    }

    private void UpdateFilterName()
    {
        var eName = (ItemCategories)_currentFilter;
        filterName.text = eName.ToString();
    }

    public void Show()
    {
        FilterInventory();
        popupMenu.SetActive(true);
    }

    public void Hide()
    {
        popupMenu.SetActive(false);
    }

    private void ShowDetails(ItemSO item)
    {
        infoContainer.SetActive(true);
        quickSlotButton.gameObject.SetActive(false);
        var buttonText = interactionButton.gameObject.GetComponentInChildren<TextMeshProUGUI>();
        var msg = "";
        switch (item.ItemCategories)
        {
            case ItemCategories.Consumable:
                msg = "Use";
                quickSlotButton.gameObject.SetActive(true);
                quickSlotButton.onClick.RemoveAllListeners();
                quickSlotButton.onClick.AddListener(delegate { quickSlot1.SaveItem(item); });
                break;
            case ItemCategories.Equipment:
                msg = "Equip";
                break;
            case ItemCategories.Material:
                msg = "Craft";
                break;
            case ItemCategories.All:
            case ItemCategories.Key:
            default:
                msg = "TBD";
                break;
        }

        buttonText.text = msg;
        interactionButton.onClick.RemoveAllListeners();
        interactionButton.onClick.AddListener(item.ItemAction);
        interactionButton.onClick.AddListener(FilterInventory);
        itemIdentifier.text = item.Identifier;
        itemCategory.text = item.ItemCategories.ToString();
        itemDescription.text = item.Description;
    }

    private void ClearDetails()
    {
        infoContainer.SetActive(false);
    }
}