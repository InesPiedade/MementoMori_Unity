using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private GameObject floatingPlatforms;
    [SerializeField] private GameObject shadowCave;
    private Player player;
    [SerializeField] private SaveController saveController;
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

    private void Start()
    {
        floatingPlatforms.SetActive(false);
        shadowCave.SetActive(true);
    }
    public void VisionOn()
    {
        floatingPlatforms.SetActive(true);
        shadowCave.SetActive(false);
    }

    public void VisionOff()
    {
        floatingPlatforms.SetActive(false);
        shadowCave.SetActive(true);
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene("Game");
        Time.timeScale = 1f;
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene("Game");
        MusicManager.instance.StopMainMenuMusic();
        saveController.ResetGame();
        Time.timeScale = 1f;
    }

    public void Quit()
    {
        Debug.Log("Exit");
        Application.Quit();
    }
}