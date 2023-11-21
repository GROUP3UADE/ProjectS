using UnityEngine;

[CreateAssetMenu(fileName = "new NPC_Data", menuName = "Scriptables/NPC/Data", order = 0)]
public class NPCData : ScriptableObject
{
    [SerializeField] private string npcName;
    [SerializeField] private Quest npcQuest;

    public string NpcName => npcName;
    public Quest NpcQuest => npcQuest;
}