using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_ItemContainer : MonoBehaviour, IPointerEnterHandler
{
    [Header("Button")] [Space(5)] [SerializeField]
    private Image buttonIcon;

    [SerializeField] private TextMeshProUGUI buttonName;
    [SerializeField] private TextMeshProUGUI buttonAmount;
    [SerializeField] private Button button;

    private ItemSO _itemData;
    public Button Button => button;

    public event Action<ItemSO> OnInteraction;

    public void SetupButton(ItemSO item, int itemAmount)
    {
        buttonIcon.sprite = item.Icon;
        buttonName.text = item.Identifier;
        buttonAmount.text = itemAmount.ToString();
        _itemData = item;
    }

    public void SetupButton(ItemSO item, string isEquipped)
    {
        buttonIcon.sprite = item.Icon;
        buttonName.text = item.Identifier;
        buttonAmount.text = isEquipped;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnInteraction?.Invoke(_itemData);
    }
}