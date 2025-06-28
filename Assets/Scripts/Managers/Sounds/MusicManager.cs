using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;
    [SerializeField] private AudioSource forestMusicTrack;
    [SerializeField] private AudioSource runMusicTrack;
    [SerializeField] private AudioSource caveMusicTrack;
    [SerializeField] private AudioSource mainMenuMusicTrack;
    [SerializeField] private AudioSource creditsMusicTrack;
    [SerializeField] private AudioSource flowerMusicTrack;
    [SerializeField] private AudioSource altarMusicTrack;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #region ForestMusic
    public void PlayForestMusic()
    {
        forestMusicTrack.Play();
    }
    public void StopForestMusic()
    {
        forestMusicTrack.Pause();
    }
    #endregion

    #region RunMusic
    public void PlayRunMusic()
    {
        runMusicTrack.Play();
    }

    public void StopRunMusic()
    {
        runMusicTrack.Pause();
    }
    #endregion

    #region CaveMusic
    public void PlayCaveMusic()
    {
        caveMusicTrack.Play();
    }

    public void StopCaveMusic()
    {
        caveMusicTrack.Pause();
    }
    #endregion

    #region MainMenuMusic
    public void PlayMainMenuMusic()
    {
        mainMenuMusicTrack.Play();
    }

    public void StopMainMenuMusic()
    {
        mainMenuMusicTrack.Pause();
    }
    #endregion

    #region creditsMusic
    public void PlayCreditsMusic()
    {
        creditsMusicTrack.Play();
    }

    public void StopCreditsMusic()
    {
        creditsMusicTrack.Pause();
    }
    #endregion

    #region FlowerMusic
    public void PlayFlowerMusic()
    {
        flowerMusicTrack.Play();
    }

    public void StopFlowerMusic()
    {
        flowerMusicTrack.Pause();
    }
    #endregion

    #region AltarMusic
    public void PlayAltarMusic()
    {
        altarMusicTrack.Play();
    }

    public void StopAltarMusic()
    {
        altarMusicTrack.Pause();
    }
    #endregion
}
