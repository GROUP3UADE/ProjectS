using System.Collections.Generic;
using UnityEngine;

public class LootDrop : MonoBehaviour
{
    [SerializeField] private Pickup pickupPrefab;
    [SerializeField] private LootTableSO lootTableSo;
    [SerializeField] private int maxDropsAmount = 10;

    private Dictionary<ItemSO, int> _buffer = new();
    private Roulette _roulette;
    private int _amountToDrop;

    private void Awake()
    {
        _amountToDrop = Random.Range(1, maxDropsAmount);
        lootTableSo.BuildTable();
        _roulette = new Roulette();
    }

    public void SpawnPickups()
    {
        if (_amountToDrop == 0) return;
        var t = transform;
        var pos = t.position;
        var rot = t.rotation;
        for (int i = 0; i < _amountToDrop; i++)
        {
            var item = _roulette.Run(lootTableSo.DicDropChance);
            if (_buffer.ContainsKey(item))
            {
                _buffer[item]++;
            }
            else
            {
                _buffer.Add(item, 1);
            }
        }

        foreach (var kvp in _buffer)
        {
            var pickup = Instantiate(pickupPrefab, pos, rot);
            pickup.Setup(kvp.Key, kvp.Value);
        }
    }
}