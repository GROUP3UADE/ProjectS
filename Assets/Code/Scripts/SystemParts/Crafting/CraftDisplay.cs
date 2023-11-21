using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftDisplay : MonoBehaviour
{
    [Header("Base UI")] [Space(5)] [SerializeField]
    private GameObject popupMenu;

    [SerializeField] private Transform contentContainer;
    [SerializeField] private UI_CraftContainer craftPrefab;

    [Header("Info UI")] [Space(5)] [SerializeField]
    private TextMeshProUGUI craftIdentifier;

    [SerializeField] private TextMeshProUGUI craftResultAmount;
    [SerializeField] private TextMeshProUGUI craftCategory;
    [SerializeField] private TextMeshProUGUI craftRequiredMaterials;
    [SerializeField] private Button craftButton;

    private List<UI_CraftContainer> _itemsGenerated = new List<UI_CraftContainer>();

    public void Show()
    {
        FilterRecipes();
        popupMenu.SetActive(true);
    }

    public void Hide()
    {
        popupMenu.SetActive(false);
    }

    private void GenerateRecipeItem(CraftRecipeSO craftSo)
    {
        var go = Instantiate(craftPrefab, contentContainer, true);
        if (!_itemsGenerated.Contains(go))
        {
            go.OnInteraction += ShowDetails;
            _itemsGenerated.Add(go);
        }

        // configura los datos que se ven en el boton
        go.SetupButton(craftSo);

        //reset the item's scale -- this can get munged with UI prefabs
        go.transform.localScale = Vector2.one;
    }

    private void FilterRecipes()
    {
        foreach (var item in _itemsGenerated)
        {
            item.OnInteraction -= ShowDetails;
            Destroy(item.gameObject);
        }

        _itemsGenerated.Clear();
        // Generador de botones anterior
        // foreach (var recipe in GameManager.Instance.CraftDatabase.Recipes)
        // {
        //     GenerateRecipeItem(recipe);
        // }
        foreach (var recipe in GameManager.Instance.CraftDatabase.UnlockedRecipes)
        {
            if (recipe.Value)
            {
                GenerateRecipeItem(recipe.Key);
            }
        }
    }

    private void ShowDetails(CraftRecipeSO recipe)
    {
        craftIdentifier.text = recipe.ItemResult.Identifier;
        craftResultAmount.text = $"x{recipe.ItemResultQuantity}";
        craftCategory.text = recipe.ItemResult.ItemCategories.ToString();
        var reqMats = "";
        for (int i = 0; i < recipe.ItemsRequired.Count; i++)
        {
            reqMats += $"{recipe.ItemsRequired[i].Identifier}  x{recipe.ItemsRequiredQuantity[i]} \n";
        }

        craftRequiredMaterials.text = reqMats;
        craftButton.onClick.RemoveAllListeners();
        craftButton.onClick.AddListener(delegate { Craft(recipe); });
    }

    private void Craft(CraftRecipeSO recipe)
    {
        // Guardar los items que son necesarios para la receta(estan en la receta)
        Dictionary<ItemSO, int> requiredItemsAmount = new();
        for (int i = 0; i < recipe.ItemsRequired.Count; i++)
        {
            var required = recipe.ItemsRequired[i];
            var requiredQuantity = recipe.ItemsRequiredQuantity[i];
            requiredItemsAmount.TryAdd(required, requiredQuantity);
        }

        // Comprobar que el jugador tenga los items
        var gotItems = false;
        foreach (var itemRequiredKVP in requiredItemsAmount)
        {
            if (!GameManager.Instance.PlayerInventory.CheckItemSO(itemRequiredKVP.Key, itemRequiredKVP.Value))
            {
                break;
            }

            gotItems = true;
        }

        // Remover los items del inventario del jugador
        if (gotItems)
        {
            foreach (var itemRequiredKVP in requiredItemsAmount)
            {
                GameManager.Instance.PlayerInventory.RemoveItemSO(itemRequiredKVP.Key, itemRequiredKVP.Value);
            }

            // Otorgarle el item deseado
            GameManager.Instance.PlayerInventory.AddItemSO(recipe.ItemResult, recipe.ItemResultQuantity);
            var msgGood = $"Se crafteo {recipe.ItemResult.Identifier} x{recipe.ItemResultQuantity}";
            GameManager.Instance.PopupManager.ShowMessage(msgGood);
            Debug.LogWarning(msgGood);
        }
        else
        {
            // Avisar que el craft fallo
            var msgBad = "Faltan materiales";
            GameManager.Instance.PopupManager.ShowMessage(msgBad);
            Debug.LogWarning(msgBad);
        }
    }
}