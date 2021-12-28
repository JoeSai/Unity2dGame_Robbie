using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;


public class AudioManager : MonoBehaviour
{
    private static AudioManager _audioManager;

    [Header("环境声音")] 
    [SerializeField] private AudioClip ambientClip;
    [SerializeField] private AudioClip musicClip;

    [Header("FX音效")]
    [SerializeField] private AudioClip deathFXClip;
    [SerializeField] private AudioClip orbFXClip;

    [Header("Robbie音效")]
    [SerializeField] private AudioClip[] walkStepClips;
    [SerializeField] private AudioClip[] crouchStepClips;
    [SerializeField] private AudioClip jumpClip;
    [SerializeField] private AudioClip jumpVoiceClip;
    [SerializeField] private AudioClip deathClip;
    [SerializeField] private AudioClip deathVoiceClip;
    [SerializeField] private AudioClip orbVoiceClip;
    
    private AudioSource ambientSource;
    private AudioSource musicSource;
    private AudioSource fxSource;
    private AudioSource playerSource;
    private AudioSource voiceSource;
    
    private void Awake()
    {
        if (_audioManager != null)
        {
            Destroy(gameObject);
            return;
        }
        _audioManager = this;
        
        DontDestroyOnLoad(gameObject);

        ambientSource = gameObject.AddComponent<AudioSource>();
        musicSource = gameObject.AddComponent<AudioSource>();
        fxSource = gameObject.AddComponent<AudioSource>();
        playerSource = gameObject.AddComponent<AudioSource>();
        voiceSource = gameObject.AddComponent<AudioSource>();

        StartLevelAudio();

    }

    void StartLevelAudio()
    {
        _audioManager.ambientSource.clip = _audioManager.ambientClip;
        _audioManager.ambientSource.loop = true;
        _audioManager.ambientSource.Play();

        _audioManager.musicSource.clip = _audioManager.musicClip;
        _audioManager.musicSource.loop = true;
        _audioManager.musicSource.Play();
    }

    public static void PlayFootstepAudio()
    {
        int index = Random.Range(0, _audioManager.walkStepClips.Length);
        _audioManager.playerSource.clip = _audioManager.walkStepClips[index];
        _audioManager.playerSource.Play();
    }
    
    public static void PlayCrouchFootstepAudio()
    {
        int index = Random.Range(0, _audioManager.crouchStepClips.Length);
        _audioManager.playerSource.clip = _audioManager.crouchStepClips[index];
        _audioManager.playerSource.Play();
    }

    public static void PlayJumpAudio()
    {
        _audioManager.playerSource.clip = _audioManager.jumpClip;
        _audioManager.playerSource.Play();

        _audioManager.voiceSource.clip = _audioManager.jumpVoiceClip;
        _audioManager.voiceSource.Play();
    }

    public static void PlayDeathAudio()
    {
        _audioManager.playerSource.clip = _audioManager.deathClip;
        _audioManager.playerSource.Play();

        _audioManager.voiceSource.clip = _audioManager.deathVoiceClip;
        _audioManager.voiceSource.Play();

        _audioManager.fxSource.clip = _audioManager.deathFXClip;
        _audioManager.fxSource.Play();
    }

    public static void PlayOrbAudio()
    {
        _audioManager.fxSource.clip = _audioManager.orbFXClip;
        _audioManager.fxSource.Play();

        _audioManager.voiceSource.clip = _audioManager.orbVoiceClip;
        _audioManager.voiceSource.Play();
    }
}
