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
}
