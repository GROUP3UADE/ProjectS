using UnityEngine;

[CreateAssetMenu(fileName = "new Item", menuName = "Scriptables/Item/Action/Healing", order = 0)]
public class HealingAction : ItemAction
{
    [SerializeField] private int healAmount;

    public override void Execute()
    {
        GameManager.Instance.Player.Heal(healAmount);
    }
}