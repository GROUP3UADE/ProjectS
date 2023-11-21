using System;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyTypes
{
    Zombie,
    Mutant,
    Survivor
}

public class KillCountManager : MonoBehaviour
{
    public Dictionary<EnemyTypes, int> KillCountTracker { get; private set; } = new Dictionary<EnemyTypes, int>();

    private void Start()
    {
        foreach (EnemyTypes enemy in Enum.GetValues(typeof(EnemyTypes)))
        {
            KillCountTracker.Add(enemy, 0);
        }
    }

    public void CountUpdate(EnemyTypes type)
    {
        KillCountTracker[type]++;
    }
}