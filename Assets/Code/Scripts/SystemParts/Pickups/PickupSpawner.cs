using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    // Hacer ruleta de probabilidad para el droprate del loot
    private Roulette _roulette;
    [SerializeField] private Pickup pickupPrefab;
    [SerializeField] private LootTableSO lootTableSo;
    [SerializeField] private int dropsAmount = 10;
    [SerializeField] private float timer = 2f;

    private Dictionary<ItemSO, int> _buffer = new();
    private Pickup _isSpawned;

    private void Awake()
    {
        lootTableSo.BuildTable();
        _roulette = new Roulette();
    }

    private void Update()
    {
        if (_isSpawned == null)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                _buffer.Clear();
                SpawnPickups();
                timer = 2f;
            }
        }
    }

    [ContextMenu("Spawn Pickups")]
    private void SpawnPickups()
    {
        var t = transform;
        var pos = t.position;
        var rot = t.rotation;
        for (int i = 0; i < dropsAmount; i++)
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
            _isSpawned = pickup;
        }
    }
}