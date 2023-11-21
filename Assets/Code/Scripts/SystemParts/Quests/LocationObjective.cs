using Unity.Mathematics;
using UnityEngine;

[CreateAssetMenu(fileName = "new Location", menuName = "Scriptables/Quests/QuestObjective/Location", order = 0)]
public class LocationObjective : QuestObjective
{
    [SerializeField] private QuestLocationData locationPrefab;
    private GameObject _prefabSpawned;
    private QuestLocationData _prefabData;

    public override bool CheckProgress()
    {
        return _prefabData.IsLocationReached;
    }

    public override void IncompleteStatus()
    {
        Debug.Log("Location was not reached. Keep searching");
    }

    public override void ResolveRequirements()
    {
        // Delete prefab
        Destroy(_prefabSpawned);
    }

    public override void Setup()
    {
        // Instantiate prefab
        var locationObj = locationPrefab.gameObject;
        _prefabSpawned = Instantiate(locationObj, locationObj.transform.position, quaternion.identity);
        _prefabData = _prefabSpawned.GetComponent<QuestLocationData>();
    }
}