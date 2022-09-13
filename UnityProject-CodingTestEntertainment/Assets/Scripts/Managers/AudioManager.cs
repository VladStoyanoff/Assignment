using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] AudioClip shootingSFX;
    [SerializeField][Range(0f, 1f)] float shootingSFXVolume;

    [Header("Target Hit")]
    [SerializeField] AudioClip targetHit;
    [SerializeField][Range(0f, 1f)] float targetHitSFXVolume;

    [Header("UI Clicked")]
    [SerializeField] AudioClip uiClicked;
    [SerializeField][Range(0f, 1f)] float uiClickedSFXVolume;

    [Header("Bomb Hit")]
    [SerializeField] AudioClip bombHit;
    [SerializeField][Range(0f, 1f)] float bomgHitSFXVolume;

    [SerializeField] Slider volumeSlider;

    [SerializeField] AudioSource gameMusic;
    [SerializeField] AudioSource endMenuMusic;

    public static AudioManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlayShootingClip()
    {
        PlayClip(shootingSFX, shootingSFXVolume);
    }

    public void PlayUIClickedClip()
    {
        PlayClip(uiClicked, uiClickedSFXVolume);
    }

    public void PlayBombHitClip()
    {
        PlayClip(bombHit, bomgHitSFXVolume);
    }

    public void PlayTargetHitClip()
    {
        PlayClip(targetHit, targetHitSFXVolume);
    }

    public void StartEndMenuMusic()
    {
        gameMusic.enabled = false;
        endMenuMusic.enabled = true;
    }

    public void StartGameMusic()
    {
        gameMusic.enabled = true;
        endMenuMusic.enabled = false;
    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
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
