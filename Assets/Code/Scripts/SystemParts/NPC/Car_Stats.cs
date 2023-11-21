using UnityEngine;

[CreateAssetMenu(fileName = "new NPC_Car_Stats", menuName = "Scriptables/NPC/Car Stats", order = 0)]
public class Car_Stats : ScriptableObject
{
    [SerializeField] private float speed;
    [SerializeField] private ItemSO key;

    public float Speed => speed;
    public ItemSO Key => key;
}