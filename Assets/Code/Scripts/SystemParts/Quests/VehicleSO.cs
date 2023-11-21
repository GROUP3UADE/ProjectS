using UnityEngine;

[CreateAssetMenu(fileName = "new Vehicle", menuName = "Scriptables/Quests/Vehicle", order = -10)]
public class VehicleSO : ScriptableObject
{
    [SerializeField] private float vehicleSpeed;
    [SerializeField] private Sprite vehicleSprite;
    [SerializeField] private ItemSO vehicleKey;

    public ItemSO VehicleKey => vehicleKey;
    public Sprite VehicleSprite => vehicleSprite;
    public float VehicleSpeed => vehicleSpeed;
}