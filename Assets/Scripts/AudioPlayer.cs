using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] Transform audioListener;
    Vector3 audioListenerPosition;

    [Header("Master Volume")]
    [SerializeField] float soundVolume = 0.5f;
    [SerializeField] Slider soundVolumeSlider;
    [SerializeField] Slider musicVolumeSlider;

    [Header("Shoot")]
    [SerializeField] AudioClip shootingClip;
    [SerializeField, Range(0f, 1f)] float shootingClipVolume = 1f;

    [Header("Explosion")]
    [SerializeField] AudioClip explosionClip;
    [SerializeField, Range(0f, 1f)] float explosionClipVolume = 1f;

    [Header("Announcer")]
    [SerializeField] AudioClip announcerClip;
    [SerializeField, Range(0f, 1f)] float announcerClipVolume = 1f;

    [Header("Pickup")]
    [SerializeField] AudioClip pickupClip;
    [SerializeField, Range(0f, 1f)] float pickupClipVolume = 1f;

    [Header("Engine")]
    [SerializeField] AudioSource engineAudioSource;
    [SerializeField, Range(0f, 1f)] float engineSourceVolume = 1f; 


    AudioSource audioSource;
    ScoreKeeper scoreKeeper;
    static AudioPlayer instance;
    
    void Awake()
    {
        ManageSingleton();
        audioSource = GetComponent<AudioSource>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    void Start()
    {
        audioListenerPosition = audioListener.position;
    }

    void Update()
    {
        if (scoreKeeper != null && !scoreKeeper.GetPlayerState())
            engineAudioSource.gameObject.SetActive(false);
        else
            engineAudioSource.gameObject.SetActive(true);

        if (soundVolumeSlider != null)
        {
            float volume = soundVolumeSlider.value/100;
            soundVolume = volume;

            if (engineAudioSource != null)
                engineAudioSource.volume = volume * engineSourceVolume;

        }
            
        if(musicVolumeSlider != null)
            audioSource.volume = musicVolumeSlider.value/100;
            
    }

    public void PlayShootingClip()
    {
        if (shootingClip != null)
        {
            AudioSource.PlayClipAtPoint(shootingClip, audioListenerPosition, shootingClipVolume * soundVolume);
        }
    }

    public void PlayExplosionClip()
    {
        if (explosionClip != null)
        {
            AudioSource.PlayClipAtPoint(explosionClip, audioListenerPosition, explosionClipVolume * soundVolume);
        }
    }

    public void PlayAnnouncerClip()
    {
        if (announcerClip != null)
        {
            AudioSource.PlayClipAtPoint(announcerClip, audioListenerPosition, announcerClipVolume * soundVolume);
        }
    }
    public void PlayPickupClip()
    {
        if (pickupClip != null)
        {
            AudioSource.PlayClipAtPoint(pickupClip, audioListenerPosition, pickupClipVolume * soundVolume);
        }
    }

    void ManageSingleton()
    {
        if (instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

}
