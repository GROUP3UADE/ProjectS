using UnityEngine;

public class Vehicle : MonoBehaviour
{
    [SerializeField] private VehicleSO vehicleStats;

    private void Start()
    {
        var spriteR = GetComponent<SpriteRenderer>();
        spriteR.sprite = vehicleStats.VehicleSprite;
        var coll = GetComponent<BoxCollider2D>();
        coll.isTrigger = true;
        coll.offset = new Vector2(0, 0);
        var bounds = spriteR.bounds;
        var lossyScale = transform.lossyScale;
        coll.size = new Vector3(bounds.size.x / lossyScale.x,
            bounds.size.y / lossyScale.y,
            bounds.size.z / lossyScale.z);
    }
}