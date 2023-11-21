using System;
using UnityEngine;

[CreateAssetMenu(fileName = "new TurnIn", menuName = "Scriptables/Quests/QuestObjective/Turn-In", order = 0)]
public class TurnInObjective : QuestObjective
{
    [SerializeField] private ItemSO itemRequired;
    [SerializeField] private int itemQuantity;

    public int CurrItemAmount { get; set; }

    private void Awake()
    {
        ObjectiveType = ObjectiveType.TurnIn;
    }

    public override bool CheckProgress()
    {
        CurrItemAmount = GameManager.Instance.PlayerInventory.CheckItemSO(itemRequired);
        return GameManager.Instance.PlayerInventory.CheckItemSO(itemRequired, itemQuantity);
    }

    public override void ResolveRequirements()
    {
        GameManager.Instance.PlayerInventory.RemoveItemSO(itemRequired, itemQuantity);
    }

    public override void IncompleteStatus()
    {
        var remainingItems = Math.Max(itemQuantity - CurrItemAmount, 0);
        Debug.Log($"You still need to get x{remainingItems} {itemRequired.Identifier}");
    }

    public override void Setup()
    {
        base.Setup();
    }
}