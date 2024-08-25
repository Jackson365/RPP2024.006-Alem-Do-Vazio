using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    
    public AudioSource musicSource, SfxSource;
    public AudioClip clipJump, clipCollectibles;

    private void OnEnable()
    {
        AudioObserver.PlayMusicEvent += PlayMusic;
        AudioObserver.StopMusicEvent += StopMusic;
        AudioObserver.PlaySfxEvent += PlaySoundEffect;
    }

    private void OnDisable()
    {
        AudioObserver.PlayMusicEvent -= PlayMusic;
        AudioObserver.StopMusicEvent -= StopMusic;
        AudioObserver.PlaySfxEvent -= PlaySoundEffect;
    }

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }  
    }

    void PlaySoundEffect(string nameClip)
    {
        switch (nameClip)
        {
            case "Jump":
                SfxSource.PlayOneShot(clipJump);
                break;
            case "Collectibles":
                SfxSource.PlayOneShot(clipCollectibles);
                break;
            default:
                Debug.LogError($"Efeito sonoro {nameClip} não encontrado");
                break;
        }
    }

    void PlayMusic()
    {
        musicSource.Play();
    }

    void StopMusic()
    {
        musicSource.Stop();
    }
}
