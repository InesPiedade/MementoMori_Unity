using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;
    public static event Action OnGameOver;
    public enum Screens { Pause, GameOver, Win, GameUi, Cutscene1 }
    private Dictionary<Screens, GameObject> organize;

    #region Declarations
    [Header("References")]
    private bool isPause = false;


    [Header("Game Objects")]
    private Player player;
    [SerializeField] private Image healthBar;
    [SerializeField] private GameObject pause;
    [SerializeField] private GameObject gameUi;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject cutscene1;
    [SerializeField] private VideoPlayer video1;
    [SerializeField] private GameObject flowerText;
    [SerializeField] private GameObject altarText;
    [SerializeField] private GameObject itemFlowerText;

    #endregion

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
        //altarText.SetActive(false);
        //itemFlowerText.SetActive(false);

        player = GameObject.FindObjectOfType<Player>();

        organize = new Dictionary<Screens, GameObject>();
        organize.Add(Screens.Pause, pause);
        organize.Add(Screens.GameUi, gameUi);
        organize.Add(Screens.Cutscene1, cutscene1);
        organize.Add(Screens.GameOver, gameOver);
    }

    private void Update()
    {


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPause)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
        if (cutscene1.activeSelf)
        {
            if (!video1.isPlaying)
            {
                StartCutscene1();
            }
        }
        else
        {
            if (video1.isPlaying)
            {
                EndCutscene1();
            }
        }
    }

    public void Resume()
    {
        pause.SetActive(false);
        ShowPanel(Screens.GameUi);
        player.enabled = true;
        Time.timeScale = 1f;
        isPause = false;
    }

    public void Pause()
    {
        ShowPanel(Screens.Pause);
        gameUi.SetActive(false);
        player.enabled = false;
        Time.timeScale = 0f;
        isPause = true;
    }

    /// ////////////////////
    public void StartCutscene1()
    {
        ShowPanel(Screens.Cutscene1);
        video1.Play();
        gameUi.SetActive(false);
        player.enabled = false;
        isPause = true;
    }

    /// ///////////////

    public void EndCutscene1()
    {
        video1.Pause();
        cutscene1.SetActive(false);
        ShowPanel(Screens.GameUi);
        player.enabled = true;
        isPause = false;
        //altarText.SetActive(true);
        //flowerText.SetActive(false);
        //itemFlowerText.SetActive(true);
    }

    /// ///////////////

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void Options()
    {
        SceneManager.LoadScene("Options");
    }

    public void Quit()
    {
        Debug.Log("Exit");
        Application.Quit();
    }

    public void HealthBar(int health, int maxHealth)
    {
        healthBar.fillAmount = (float)health / (float)maxHealth;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
        Time.timeScale = 1f;
    }

    public void GameOver()
    {
        OnGameOver?.Invoke();
        ShowPanel(Screens.GameOver);
        gameUi.SetActive(false);
        player.enabled = false;
        Time.timeScale = 0f;
        isPause = true;
    }

    public void ShowPanel(Screens panelType)
    {
        foreach (var panel in organize.Values)
        {
            if (panel != null) panel.SetActive(false);
        }
        if (organize.ContainsKey(panelType) && organize[panelType] != null)
        {
            organize[panelType].SetActive(true);
        }
    }
}
