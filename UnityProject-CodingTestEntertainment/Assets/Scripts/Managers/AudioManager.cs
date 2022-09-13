using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] AudioClip shootingSFX;
    [SerializeField][Range(0f, 1f)] float shootingSFXVolume;

    [Header("Target Hit")]
    [SerializeField] AudioClip targetHit;
    [SerializeField][Range(0f, 1f)] float targetHitSFXVolume;

    public static AudioManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There's more than one AudioManager! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void PlayShootingClip()
    {
        PlayClip(shootingSFX, shootingSFXVolume);
    }

    public void PlayTargetHitClip()
    {
        PlayClip(targetHit, targetHitSFXVolume);
    }

    void PlayClip(AudioClip clip, float volume)
    {
        if (clip != null)
        {
            Vector3 cameraPos = Camera.main.transform.position;
            AudioSource.PlayClipAtPoint(clip, cameraPos, volume);
        }
    }
}
