using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private GameObject floatingPlatforms;
    [SerializeField] private GameObject shadowCave;
    [SerializeField] private GameObject instructionsPanel;
    private Player player;
    [SerializeField] private SaveController saveController;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(instructionsPanel);
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
        //instructionsPanel.SetActive(true);

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
        saveController.LoadGame();
        instructionsPanel.SetActive(false);
        Cursor.visible = false;
        Time.timeScale = 1f;
    }

    public void NewGame()
    {
        SceneManager.LoadScene("Game");
        Time.timeScale = 1f;
        Cursor.visible = true;
    }

    public void Instructions()
    {
        instructionsPanel.SetActive(true);
        saveController.ResetGame();
        MusicManager.instance.StopMainMenuMusic();
        MusicManager.instance.StopCaveMusic();
        MusicManager.instance.StopForestMusic();
        MusicManager.instance.StopRunMusic();
        Cursor.visible = true;
        Time.timeScale = 1f;
    }

    public void Quit()
    {
        Debug.Log("Exit");
        Application.Quit();
    }
}