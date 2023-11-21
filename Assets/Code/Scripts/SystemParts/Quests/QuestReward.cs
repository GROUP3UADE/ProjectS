using UnityEngine;

[CreateAssetMenu(fileName = "new reward", menuName = "Scriptables/Quests/QuestReward", order = 0)]
public class QuestReward : ScriptableObject
{
    [SerializeField] private ItemSO[] itemReward;
    [SerializeField] private int[] rewardQuantity;

    public ItemSO[] ItemReward => itemReward;
    public int[] RewardQuantity => rewardQuantity;
}