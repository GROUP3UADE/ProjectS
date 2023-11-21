using System;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class QuestLocationData : MonoBehaviour
{
    public bool IsLocationReached { get; private set; }
    private CircleCollider2D _circleCollider2D;
    [SerializeField] private float radius = 1;


    private void Awake()
    {
        _circleCollider2D = GetComponent<CircleCollider2D>();
        _circleCollider2D.radius = radius;
        _circleCollider2D.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // La vieja confiable forma de desactivar multiples activaciones
        // if (IsLocationReached) return;
        if (other.CompareTag("PlayerDetection"))
        {
            print("Location Reached");
            IsLocationReached = true;
            // Pseudo forma 1 de desactivar subsecuentes activaciones?????
            _circleCollider2D.radius = 0;
            // Pseudo forma 2
            _circleCollider2D.enabled = false;
        }
    }
}