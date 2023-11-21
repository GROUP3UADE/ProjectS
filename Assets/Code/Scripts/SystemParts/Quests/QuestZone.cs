using UnityEngine;

public class QuestZone : MonoBehaviour
{
    [SerializeField] private Quest quest;

    private void OnTriggerEnter2D(Collider2D other)
    {
        var gmQm = GameManager.Instance.QuestManager;
        if (other.CompareTag("PlayerDetection"))
        {
            if (quest.QuestState == QuestState.NotActive)
            {
                gmQm.AddQuest(quest);
            }

            else if (quest.QuestState == QuestState.Active)
            {
                gmQm.TryCompleteQuest(quest);
            }

            else if (quest.QuestState == QuestState.Rewarded)
            {
                // Recompensa
                print("Rewards obtained");
            }
        }
    }
}