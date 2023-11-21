using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioSource ambienceMusic;
    [SerializeField] private AudioSource pickUpSound;
    [SerializeField] private AudioSource inventorySound;
    [SerializeField] private AudioSource swordSound;
    [SerializeField] private AudioSource damageSound;
    [SerializeField] private AudioSource zombieSound;
    [SerializeField] private AudioSource mutantSound;
    [SerializeField] private AudioSource tenseMusic;
    [SerializeField] private AudioSource punchSound;
    [SerializeField] private AudioSource blacksmithSound;
    [SerializeField] private AudioSource eSound;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayAmbienceMusic()
    {
        ambienceMusic.Play();
    }

    public void PlayPickUpSound()
    {
        pickUpSound.Play();
    }

    public void PlayInteractSound()
    {
        eSound.Play();
    }

    public void PlayInventorySound()
    {
        inventorySound.Play();
    }

    public void PlaySwordSound()
    {
        swordSound.Play();
    }

    public void PlayDamageSound()
    {
        damageSound.Play();
    }

    public void PlayZombieSound()
    {
        zombieSound.Play();
    }

    public void PlayMutantSound()
    {
        mutantSound.Play();
    }

    public void PlayTenseMusic()
    {
        tenseMusic.Play();
    }

    public void PlayPunchSound()
    {
        punchSound.Play();
    }

    public void FadeBlacksmithSound(float distanceToBlacksmith, float maxAudibleDistance)
    {
        if (distanceToBlacksmith > maxAudibleDistance)
        {
            // Si el jugador est� fuera del rango m�ximo de audici�n, det�n el sonido del herrero.
            blacksmithSound.Stop();
        }
        else
        {
            // Calcula el volumen en funci�n de la distancia.
            float volume = 1.0f - Mathf.Clamp(distanceToBlacksmith / maxAudibleDistance, 0.0f, 1.0f);
            blacksmithSound.volume = volume;

            // Si el sonido no est� reproduci�ndose, comienza la reproducci�n.
            if (!blacksmithSound.isPlaying)
            {
                blacksmithSound.Play();
            }
        }
    }
}