using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VolumeObserver 
{
    public static event Action<float> VolumeChanged;

    private static float currentVolume = 1.0f; 

    public static float CurrentVolume
    {
        get => currentVolume;
        set
        {
            currentVolume = Mathf.Clamp(value, 0.0f, 1.0f); 
            NotifyVolumeChange();
        }
    }

    private static void NotifyVolumeChange()
    {
        VolumeChanged?.Invoke(currentVolume);
    }
}

