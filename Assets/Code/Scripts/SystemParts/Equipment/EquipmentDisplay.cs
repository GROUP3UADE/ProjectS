using Character;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentDisplay : MonoBehaviour
{
    #region Serializables

    [SerializeField] private PlayerController _playerController;
    [SerializeField] private GameObject popupMenu;
    [SerializeField] private List<UI_EquipSlotContainer> equippedItemSlots;

    [Header("Equipped Item Info")] [Space(5)] [SerializeField]
    private TextMeshProUGUI equippedIdentifier;

    [SerializeField] private TextMeshProUGUI equippedSlot;
    [SerializeField] private TextMeshProUGUI equippedDescription;
    [SerializeField] private TextMeshProUGUI equippedStats;
    [SerializeField] private Button equippedUnequipButton;

    [Header("Compared Item Info")] [Space(5)] [SerializeField]
    private TextMeshProUGUI comparedIdentifier;

    [SerializeField] private TextMeshProUGUI comparedSlot;
    [SerializeField] private TextMeshProUGUI comparedDescription;
    [SerializeField] private TextMeshProUGUI comparedStats;
    [SerializeField] private Button comparedEquipButton;

    [Header("Compared Item Display")] [Space(5)] [SerializeField]
    private UI_ItemContainer itemPrefab;

    [SerializeField] private Transform contentContainer;
    [SerializeField] private TextMeshProUGUI filterName;

    [Header("Total Equipped Stats")] [Space(5)] [SerializeField]
    private TextMeshProUGUI totalArmorEquipped;

    [SerializeField] private TextMeshProUGUI totalStrengthEquipped;

    #endregion

    #region Miembros privados

    private List<UI_ItemContainer> _itemsGenerated = new List<UI_ItemContainer>();
    private int _currentFilter;
    private int _lengthFilter;
    private EquipmentDatabase _equipmentDatabase;
    
    #endregion

    private void Awake()
    {
        _equipmentDatabase = GetComponent<EquipmentDatabase>();
    }

    public void Show()
    {
        _lengthFilter = _equipmentDatabase.EquippedItems.Count - 1;
        popupMenu.SetActive(true);
        UpdateEquipmentUI();
    }

    private void UpdateEquipmentUI()
    {
        DisplayEquippedItems();
        DisplayComparedItems();
        DisplayTotalStatsEquipped();
    }

    public void Hide()
    {
        popupMenu.SetActive(false);
    }

    [ContextMenu("DisplaySlots")]
    public void DisplayEquippedItems()
    {
        var dic = _equipmentDatabase.EquippedItems;
        var count = 0;
        foreach (var keyV in dic)
        {
            if (keyV.Value != null)
            {
                equippedItemSlots[count].ButtonText.text = keyV.Value.Identifier;
                equippedItemSlots[count].ButtonIcon.sprite = keyV.Value.Icon;
                var button = equippedItemSlots[count].Button;
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(delegate { DisplayEquippedInfo(keyV.Value, button); });
            }
            else
            {
                equippedItemSlots[count].ButtonText.text = keyV.Key.ToString();
            }

            count++;
        }
    }

    public void DisplayEquippedInfo(ItemSO item, Button buttonRefe)
    {
        equippedIdentifier.text = item.Identifier;
        var stats = (EquipmentStatsSO)item.Stats;
        equippedSlot.text = stats.EquipSlot.ToString();
        equippedDescription.text = item.Description;
        equippedStats.text = $"Armor: {stats.Armor} \n Strength: {stats.Strength} \n";
        equippedUnequipButton.onClick.RemoveAllListeners();
        equippedUnequipButton.onClick.AddListener(delegate { UnequipDelegate(stats.EquipSlot, buttonRefe); });
    }

    public void ClearEquippedInfo()
    {
        equippedIdentifier.text = "";
        equippedSlot.text = "";
        equippedDescription.text = "";
        equippedStats.text = "";
        equippedUnequipButton.onClick.RemoveAllListeners();
    }

    private void UnequipDelegate(EquipmentSlots slot, Button buttonRefe)
    {
        _equipmentDatabase.RemoveEquipment(slot);
        buttonRefe.onClick.RemoveAllListeners();
        UpdateEquipmentUI();
        ClearEquippedInfo();
    }

    private void EquipDelegate(ItemSO item)
    {
        _equipmentDatabase.PutEquipmentOn(item);
        if (((EquipmentStatsSO)item.Stats).EquipSlot == EquipmentSlots.MainHand) _playerController.EquipWeapon(((EquipmentWeapon)item).WeaponPrefab);
        UpdateEquipmentUI();
    }

    public void DisplayComparedInfo(ItemSO item)
    {
        comparedIdentifier.text = item.Identifier;
        var stats = (EquipmentStatsSO)item.Stats;
        comparedSlot.text = stats.EquipSlot.ToString();
        comparedDescription.text = item.Description;
        comparedStats.text = $"Armor: {stats.Armor} \n Strength: {stats.Strength} \n";
        comparedEquipButton.onClick.RemoveAllListeners();
        comparedEquipButton.onClick.AddListener(delegate { EquipDelegate(item); });
    }

    public void DisplayComparedItems()
    {
        UpdateFilterName();
        FilterInventory();
    }

    private void GenerateInventoryItem(ItemSO itemSo)
    {
        var go = Instantiate(itemPrefab, contentContainer, true);
        if (!_itemsGenerated.Contains(go))
        {
            go.Button.onClick.AddListener(delegate { DisplayComparedInfo(itemSo); });
            _itemsGenerated.Add(go);
        }

        var stats = (EquipmentStatsSO)itemSo.Stats;
        var equippedText = "";
        if (stats.IsEquipped)
        {
            equippedText = "(E)";
        }

        // configura los datos que se ven en el boton
        go.SetupButton(itemSo, equippedText);

        //reset the item's scale -- this can get munged with UI prefabs
        go.transform.localScale = Vector2.one;
    }

    private void FilterInventory()
    {
        foreach (var item in _itemsGenerated)
        {
            item.Button.onClick.RemoveAllListeners();
            Destroy(item.gameObject);
        }

        _itemsGenerated.Clear();
        foreach (var kvP in GameManager.Instance.PlayerInventory.PlayerInventoryDicSO)
        {
            if (kvP.Key.ItemCategories == ItemCategories.Equipment)
            {
                var stats = (EquipmentStatsSO)kvP.Key.Stats;
                if (_equipmentDatabase.EquippedItems.ElementAt(_currentFilter).Key == stats.EquipSlot)
                {
                    GenerateInventoryItem(kvP.Key);
                }
            }
        }
    }

    private void UpdateFilterName()
    {
        filterName.text = _equipmentDatabase.EquippedItems.ElementAt(_currentFilter).Key.ToString();
    }

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

    private void DisplayTotalStatsEquipped()
    {
        totalArmorEquipped.text = _equipmentDatabase.Armor.ToString();
        totalStrengthEquipped.text = _equipmentDatabase.Strength.ToString();
    }
}