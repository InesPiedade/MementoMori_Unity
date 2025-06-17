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
    public enum Screens { Pause, GameOver, Win, GameUi, Inventory, Cutscene1, Cutscene2}
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
    [SerializeField] private GameObject inventory;

    [SerializeField] private GameObject cutscene1;
    [SerializeField] private GameObject cutscene2;
    [SerializeField] private VideoPlayer video1;
    [SerializeField] private VideoPlayer video2;

    [SerializeField] private GameObject flowerText;
    [SerializeField] private GameObject altarText;
    [SerializeField] private GameObject dogText;
    [SerializeField] private GameObject caveText;
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
        player = GameObject.FindObjectOfType<Player>();

        organize = new Dictionary<Screens, GameObject>();
        organize.Add(Screens.Pause, pause);
        organize.Add(Screens.GameUi, gameUi);
        organize.Add(Screens.Inventory, inventory);
        organize.Add(Screens.Cutscene1, cutscene1);
        organize.Add(Screens.Cutscene2, cutscene2);
        organize.Add(Screens.GameOver, gameOver);

        flowerText.SetActive(true);
        dogText.SetActive(false);
        caveText.SetActive(false);
        altarText.SetActive(false);
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

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (isPause)
            {
                CloseInventory();
            }
            else
            {
                OpenInventory();
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

    public void OpenInventory()
    {
        ShowPanel(Screens.Inventory);
        gameUi.SetActive(false);
        player.enabled = false;
        Time.timeScale = 0f;
        isPause = true;
    }

    public void CloseInventory()
    {
        pause.SetActive(false);
        ShowPanel(Screens.GameUi);
        player.enabled = true;
        Time.timeScale = 1f;
        isPause = false;
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

    public void StartCutscene2()
    {
        ShowPanel(Screens.Cutscene2);
        video2.Play();
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
    }

    public void EndCutscene2()
    {
        video2.Pause();
        cutscene2.SetActive(false);
        ShowPanel(Screens.GameUi);
        player.enabled = true;
        isPause = false;
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

    public void HealthBar(int health, int maxHealth)
    {
        healthBar.fillAmount = (float)health / (float)maxHealth;
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

    public void ObjectiveFlower()
    {
        flowerText.SetActive(true);
        dogText.SetActive(false);
        caveText.SetActive(false);
        altarText.SetActive(false);
    }
    public void ObjectiveDog()
    {
        dogText.SetActive(true);
        caveText.SetActive(false);
        altarText.SetActive(false);
        flowerText.SetActive(false);
    }
    public void ObjectiveCave()
    {
        caveText.SetActive(true);
        altarText.SetActive(false);
        flowerText.SetActive(false);
        dogText.SetActive(false);
    }
    public void ObjectiveAltar()
    {
        altarText.SetActive(true);
        caveText.SetActive(false);
        dogText.SetActive (false);
        flowerText.SetActive (false);
    }
}
