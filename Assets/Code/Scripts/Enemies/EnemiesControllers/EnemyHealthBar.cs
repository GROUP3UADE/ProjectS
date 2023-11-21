using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Camera camera;
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    private void Awake()
    {
        // Busca y asigna las referencias a la cámara y al objetivo
        camera = Camera.main;
        target = transform.parent;
    }
    
    public void UpdateHealthBar(float currentValue, float maxValue)
    {
        slider.value = currentValue / maxValue;
    }

    public void UpdateHealthBar(float ratio)
    {
        slider.value = ratio;
    }

    private void Update()
    {
        transform.rotation = camera.transform.rotation;
        target.position += offset;
    }
}