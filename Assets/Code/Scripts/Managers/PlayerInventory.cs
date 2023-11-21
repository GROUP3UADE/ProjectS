using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    private Dictionary<ItemSO, int> _playerInventorySO;
    public Dictionary<ItemSO, int> PlayerInventoryDicSO => _playerInventorySO;

    [SerializeField] private ScrollRect logContainer;
    [SerializeField] private UI_ChatPrefab chatPrefab;

    private void Awake()
    {
        _playerInventorySO = new();
    }

    // Agrega el item al inventario, si ya estaba le suma la cantidad extra, sino crea una nueva entrada y le asigna ese valor
    public void AddItemSO(ItemSO item, int addQuantity)
    {
        SendPickupLogMessage(item.Identifier, addQuantity.ToString());
        if (!_playerInventorySO.TryGetValue(item, out var oldQuantity))
        {
            _playerInventorySO.Add(item, addQuantity);
        }
        else
        {
            _playerInventorySO[item] = oldQuantity + addQuantity;
        }
    }

    // Lo mismo que agregar pero al reves
    public void RemoveItemSO(ItemSO item, int removeQuantity)
    {
        if (!_playerInventorySO.TryGetValue(item, out var oldQuantity)) return;
        if (oldQuantity <= removeQuantity)
        {
            _playerInventorySO[item] = 0;
            _playerInventorySO.Remove(item);
        }
        else
        {
            _playerInventorySO[item] = oldQuantity - removeQuantity;
        }
    }

    // Devuelve la cantidad que hay del item pedido
    public int CheckItemSO(ItemSO item)
    {
        _playerInventorySO.TryGetValue(item, out var itemQuantity);
        return itemQuantity;
    }

    // Comprueba si el item pedido tiene la cantidad adecuada
    public bool CheckItemSO(ItemSO item, int requiredQuantity)
    {
        _playerInventorySO.TryGetValue(item, out var itemQuantity);
        return itemQuantity >= requiredQuantity;
    }

    public void SendPickupLogMessage(string itemIdentifier = "", string itemAmount = "")
    {
        var msg = $"You receive item: <color=#FFFFFF> [{itemIdentifier}] </color>x{itemAmount}.";

        var go = Instantiate(chatPrefab, logContainer.content.transform);
        go.SetupText(msg);
        StartCoroutine(ApplyScrollPosition(logContainer));
    }

    IEnumerator ApplyScrollPosition(ScrollRect sr)
    {
        yield return new WaitForEndOfFrame();
        sr.verticalNormalizedPosition = 1f;
        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)sr.transform);
    }
}