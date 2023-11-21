using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private List<Quest> questDatabase;

    public event Action<Quest> OnQuestReceived;
    public event Action<Quest> OnQuestCompleted;
    public List<Quest> ActiveQuests { get; private set; } = new();
    public List<Quest> CompletedQuests { get; private set; } = new();

    private void Awake()
    {
        foreach (var q in questDatabase)
        {
            q.ChangeState(QuestState.NotActive);
        }
    }

    public void AddQuest(Quest quest)
    {
        /* Agrega una misión a la lista de misiones activas */
        if (!ActiveQuests.Contains(quest))
        {
            quest.ChangeState(QuestState.Active);
            ActiveQuests.Add(quest);
            OnQuestReceived?.Invoke(quest);
        }
    }

    public bool CheckProgress(Quest quest)
    {
        var aux = 0;
        /* Verifica el progreso de una misión */
        foreach (var obj in quest.QuestObjectives)
        {
            if (!obj.CheckProgress())
            {
                aux++;
            }
        }

        return aux == 0;
    }

    public void CompleteQuest(Quest quest)
    {
        /* Marca una misión como completada y la mueve a la lista de misiones completadas */
        if (ActiveQuests.Contains(quest))
        {
            foreach (var obj in quest.QuestObjectives)
            {
                obj.ResolveRequirements();
            }

            ActiveQuests.Remove(quest);
            if (!CompletedQuests.Contains(quest))
            {
                quest.GiveItemRewards();
                quest.ChangeState(QuestState.Rewarded);
                CompletedQuests.Add(quest);
                OnQuestCompleted?.Invoke(quest);
            }
        }
    }

    public void TryCompleteQuest(Quest quest)
    {
        if (CheckProgress(quest))
        {
            CompleteQuest(quest);
        }
        else
        {
            foreach (var obj in quest.QuestObjectives)
            {
                obj.IncompleteStatus();
            }
        }
    }
}