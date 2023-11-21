using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Loot Table", menuName = "Scriptables/Loot Table", order = 0)]
public class LootTableSO : ScriptableObject
{
    [SerializeField] private List<ItemSO> itemToDropList;
    [SerializeField] private List<int> itemDropChanceList;

    public Dictionary<ItemSO, int> DicDropChance { get; private set; }

    public void BuildTable()
    {
        DicDropChance = new Dictionary<ItemSO, int>();
        for (var i = 0; i < itemToDropList.Count; i++)
        {
            DicDropChance.Add(itemToDropList[i], itemDropChanceList[i]);
        }
    }
}