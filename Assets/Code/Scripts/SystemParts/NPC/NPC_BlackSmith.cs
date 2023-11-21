using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_BlackSmith : NPCModel
{
    [SerializeField] private Dialogue dialogueInstance;

    public override void Interaction()
    {
        var gmQm = GameManager.Instance.QuestManager;
        switch (data.NpcQuest.QuestState)
        {
            case QuestState.NotActive:
                if (dialogueInstance.isQuestAvailable)
                {
                    gmQm.AddQuest(data.NpcQuest);
                }
                break;

            case QuestState.Active:
                gmQm.TryCompleteQuest(data.NpcQuest);
                break;

            case QuestState.Complete:
                break;

            case QuestState.Rewarded:
                print("Reward recieved.");
                break;

            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
    }

    public override void OnTriggerExit2D(Collider2D other)
    {
        base.OnTriggerExit2D(other);
    }
}
