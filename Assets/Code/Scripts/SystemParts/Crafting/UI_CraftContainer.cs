using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_CraftContainer : MonoBehaviour, IPointerEnterHandler
{
    [Header("Button")] [Space(5)] [SerializeField]
    private Image buttonIcon;

    [SerializeField] private TextMeshProUGUI buttonName;

    private CraftRecipeSO _craftData;

    public event Action<CraftRecipeSO> OnInteraction;

    public void SetupButton(CraftRecipeSO recipe)
    {
        buttonIcon.sprite = recipe.ItemResult.Icon;
        buttonName.text = recipe.ItemResult.Identifier;
        _craftData = recipe;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnInteraction?.Invoke(_craftData);
    }
}