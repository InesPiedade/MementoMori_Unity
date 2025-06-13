using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] private GameObject floatingPlatforms;
    [SerializeField] private GameObject shadowCave;
    private Player player;

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
        Time.timeScale = 1f;
    }

    public void Quit()
    {
        Debug.Log("Exit");
        Application.Quit();
    }
}