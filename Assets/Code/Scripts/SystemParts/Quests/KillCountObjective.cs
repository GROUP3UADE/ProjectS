using UnityEngine;

[CreateAssetMenu(fileName = "new KillCount", menuName = "Scriptables/Quests/QuestObjective/KillCount", order = 0)]
public class KillCountObjective : QuestObjective
{
    [SerializeField] private EnemyTypes typeToKill;
    [SerializeField] private int amountToKill;

    private int _startingCount;
    private int _lastCheck;

    public override bool CheckProgress()
    {
        _lastCheck = GameManager.Instance.KillCountManager.KillCountTracker[typeToKill] - _startingCount;
        return _lastCheck >= amountToKill;
    }

    public override void IncompleteStatus()
    {
        var remainingKills = amountToKill - _lastCheck;
        Debug.Log($"You still need to get x{remainingKills} {typeToKill.ToString()} enemy kills!");
    }

    public override void ResolveRequirements()
    {
    }

    public override void Setup()
    {
        base.Setup();
        _startingCount = GameManager.Instance.KillCountManager.KillCountTracker[typeToKill];
    }
}