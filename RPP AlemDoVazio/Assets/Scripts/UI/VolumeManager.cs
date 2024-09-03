using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    public Slider volumeSlider;

    private void Start()
    {
        volumeSlider.value = VolumeObserver.CurrentVolume; 
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    private void SetVolume(float volume)
    {
        VolumeObserver.CurrentVolume = volume;
    }

    private void OnEnable()
    {
        VolumeObserver.VolumeChanged += UpdateAudioSources;
    }

    private void OnDisable()
    {
        VolumeObserver.VolumeChanged -= UpdateAudioSources; 
    }

    private void UpdateAudioSources(float newVolume)
    {
        AudioListener.volume = newVolume;
    }
}

