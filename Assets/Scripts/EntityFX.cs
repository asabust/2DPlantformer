using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityFX : MonoBehaviour
{
    private SpriteRenderer sprite;

    [Header("Flash Effects")]
    public float flashDuration = 0.2f;
    [SerializeField] private Material flashMaterial;
    private Material originalMaterial;

    private Coroutine flashCoroutine;

    private void Start()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        originalMaterial = sprite.material;
    }

    public void PlayFlashEffect()
    {
        if (flashCoroutine != null)
        {
            StopCoroutine(flashCoroutine);
        }

        flashCoroutine = StartCoroutine(FlashFX());
    }

    private IEnumerator FlashFX()
    {
        sprite.material = flashMaterial;
        yield return new WaitForSeconds(flashDuration);
        sprite.material = originalMaterial;
    }

    private void RadColorBlink()
    {
        sprite.color = sprite.color != Color.white ? Color.white : Color.red;
    }

    private void StopRadColorBlink()
    {
        CancelInvoke();
        sprite.color = Color.white;
    }
}