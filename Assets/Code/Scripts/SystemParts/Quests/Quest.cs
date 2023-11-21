using UnityEngine;

public enum QuestState
{
    NotActive,
    Active,
    Complete,
    Rewarded
}

[CreateAssetMenu(fileName = "new Quest", menuName = "Scriptables/Quests/Quest", order = 0)]
public class Quest : ScriptableObject
{
    [SerializeField] private string questName;
    [SerializeField] private QuestObjective[] questObjectives;
    [SerializeField] private QuestState questState = QuestState.NotActive;
    [SerializeField] private QuestReward questReward;

    public QuestState QuestState => questState;
    public string QuestName => questName;
    public QuestObjective[] QuestObjectives => questObjectives;
    public QuestReward QuestReward => questReward;

    public void ChangeState(QuestState newState)
    {
        questState = newState;
        if (questState == QuestState.Active)
        {
            foreach (var q in questObjectives)
            {
                q.Setup();
            }
        }
    }

    public void GiveItemRewards()
    {
        if (questReward != null && questState != QuestState.Rewarded)
        {
            var msg = $"Your rewards for completing {questName} are: \n";
            for (int i = 0; i < questReward.ItemReward.Length; i++)
            {
                var itemReward = questReward.ItemReward[i];
                var itemAmount = questReward.RewardQuantity[i];
                msg += $"- x{itemAmount} {itemReward.Identifier}\n";
                GameManager.Instance.PlayerInventory.AddItemSO(itemReward, itemAmount);
            }

            Debug.Log(msg);
        }
    }
}