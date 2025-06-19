using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundMixerManager : MonoBehaviour
{
    public static SoundMixerManager instance;

    [SerializeField] private AudioMixer AudioMixer;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetMasterVolume(float lvl)
    {
        AudioMixer.SetFloat("MasterVolume", Mathf.Log10(lvl) * 20f);
    }

    public void SetMusicVolume(float lvl)
    {
        AudioMixer.SetFloat("MusicVolume", Mathf.Log10(lvl) * 20f);
    }

    public void SetSoundFXVolume(float lvl)
    {
        AudioMixer.SetFloat("SoundFXVolume", Mathf.Log10(lvl) * 20f);
    }

    //public float GetMasterVolume()
    //{
    //    AudioMixer.GetFloat("MasterVolume", out float lvl);
    //    return Mathf.Log10(Mathf.Clamp(lvl, 0.0001f, 1f)) * 20f;
    //}

    //public float GetMusicVolume()
    //{
    //    AudioMixer.GetFloat("MusicVolume", out float lvl);
    //    return Mathf.Log10(Mathf.Clamp(lvl, 0.0001f, 1f)) * 20f;
    //}

    //public float GetSoundFXVolume()
    //{
    //    AudioMixer.GetFloat("SoundFXVolume", out float lvl);
    //    return Mathf.Log10(Mathf.Clamp(lvl, 0.0001f, 1f)) * 20f;
    //}
}
