using UnityEngine;

public enum ObjectiveType
{
    TurnIn,
    Location
}

public class QuestObjective : ScriptableObject
{
    public ObjectiveType ObjectiveType { get; protected set; }

    public virtual bool CheckProgress()
    {
        return default;
    }

    public virtual void ResolveRequirements()
    {
    }

    public virtual void IncompleteStatus()
    {
    }

    public virtual void Setup()
    {
    }
}