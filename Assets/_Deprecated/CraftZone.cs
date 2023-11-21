using System.Collections.Generic;
using UnityEngine;

public class CraftZone : MonoBehaviour
{
    [SerializeField] private ItemSO itemToCraftSO;

    private void OnTriggerEnter2D(Collider2D other)
    {
        var playerModel = other.GetComponent<PlayerModel>();
        if (playerModel)
        {
            CraftSO();
        }
    }

    private void CraftSO()
    {
        // Buscar que receta tiene ese item (se busca con el ID)
        var recipe = GameManager.Instance.CraftDatabase.GetRecipeSO(itemToCraftSO);
        // Guardar los items que son necesarios para la receta(estan en la receta)
        Dictionary<ItemSO, int> requiredItemsAmount = new();
        foreach (var required in recipe.ItemsRequired)
        {
            if (requiredItemsAmount.ContainsKey(required))
            {
                requiredItemsAmount[required]++;
            }
            else
            {
                requiredItemsAmount.Add(required, 1);
            }
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
            GameManager.Instance.PlayerInventory.AddItemSO(itemToCraftSO, recipe.ItemResultQuantity);
            Debug.LogWarning("Crafteo satisfactorio");
        }
        else
        {
            // Avisar que el craft fallo
            Debug.LogWarning("Mensaje en pantalla que no tiene items requeridos para el craft");
        }
    }
}