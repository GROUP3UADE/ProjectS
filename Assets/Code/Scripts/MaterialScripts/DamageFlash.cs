using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFlash : MonoBehaviour
{
    [SerializeField] private Color flashColor = Color.white;
    [SerializeField] private float flashTime = .25f;
    [SerializeField] private AnimationCurve flashSpeedCurve;
    
    private SpriteRenderer _spriteRenderer;
    private Material _material;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        
        Init();
    }

    private void Init()
    {
        _material = _spriteRenderer.material;
    }

    public void CallDamageFlash()
    {
        if (!gameObject.activeInHierarchy) return;
        StartCoroutine(DamageFlasher());
    }

    private IEnumerator DamageFlasher()
    {
        float currentFlashAmount = 0f;
        float elapsedTime = 0f;
        
        SetFlashColor();

        while (elapsedTime < flashTime)
        {
            elapsedTime += Time.deltaTime;

            currentFlashAmount = Mathf.Lerp(1f, flashSpeedCurve.Evaluate(elapsedTime), (elapsedTime / flashTime));
            SetFlashAmount(currentFlashAmount);
            yield return null;
        }
    }

    private void SetFlashColor()
    {
        _material.SetColor("_FlashColor", flashColor);
    }

    private void SetFlashAmount(float amount)
    {
        _material.SetFloat("_FlashValue", amount);
    }
}
