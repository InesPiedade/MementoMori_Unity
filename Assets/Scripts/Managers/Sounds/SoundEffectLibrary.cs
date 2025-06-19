using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectLibrary : MonoBehaviour
{
    //// string = sound name
    //private Dictionary<string, List<AudioClip>> soundDictionary;
    //[SerializeField] private SoundEffectGroup[] soundEffectGroups;

    //private void Awake()
    //{
    //    InicializeDictionary();
    //}

    //private void InicializeDictionary()
    //{
    //    soundDictionary = new Dictionary<string, List<AudioClips>>();
    //    foreach(SoundEffectGroup soundEffectGroup in soundEffectGroups)
    //    {
    //        soundDictionary[soundEffectGroup.name] = soundEffectGroup.audioClips;
    //    }
    //}

}

[System.Serializable]
public struct SoundEffectGroup
{
    //public string name;
    //public List<AudioClips> audioClips;
}
