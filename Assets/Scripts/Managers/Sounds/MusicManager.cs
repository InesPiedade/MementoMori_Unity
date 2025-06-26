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

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(forestMusicTrack);
            DontDestroyOnLoad(runMusicTrack);
            DontDestroyOnLoad(caveMusicTrack);
            DontDestroyOnLoad(mainMenuMusicTrack);
            DontDestroyOnLoad(creditsMusicTrack);
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
        mainMenuMusicTrack.Stop();
        caveMusicTrack.Stop();
        runMusicTrack.Stop();
        creditsMusicTrack.Stop();
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
        forestMusicTrack.Stop();
        mainMenuMusicTrack.Stop();
        caveMusicTrack.Stop();
        creditsMusicTrack.Stop();
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
        runMusicTrack.Stop();
        forestMusicTrack.Stop();
        mainMenuMusicTrack.Stop();
        creditsMusicTrack.Stop();
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
        runMusicTrack.Stop();
        forestMusicTrack.Stop();
        caveMusicTrack.Stop();
        creditsMusicTrack.Stop();
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
        mainMenuMusicTrack.Stop();
        runMusicTrack.Stop();
        forestMusicTrack.Stop();
        caveMusicTrack.Stop();
    }

    public void StopCreditsMusic()
    {
        creditsMusicTrack.Pause();
    }
    #endregion
}
