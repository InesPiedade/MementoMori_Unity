using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Flower : MonoBehaviour, IInteractable
{
    #region Declarations
    [Header("Game Objects")]
    [SerializeField] private GameObject wall;

    [SerializeField] private GameObject dog;
    //[SerializeField] private GameObject cutscene1;
    private UiManager uiManager;
    private GameManager gameManager;
    [SerializeField] private SaveController saveController;
    
    #endregion

    private void Start()
    {
        uiManager = UiManager.instance;
    }
    public void Interact()
    {
        Debug.Log("FLOWER");
        MusicManager.instance.StopRunMusic();   
        StartCoroutine(StartCutscene1());
        saveController.SaveGame();
    }

    private IEnumerator StartCutscene1()
    {
        yield return new WaitForSeconds(1f);
        uiManager.StartCutscene1();
        StartCoroutine(EndCutscene1());
    }
    private IEnumerator EndCutscene1()
    {
        yield return new WaitForSeconds(18f);
        uiManager.EndCutscene1();
        gameObject.SetActive(false);
        wall.SetActive(false);
        dog.SetActive(false);
        uiManager.ObjectiveCave();
    }
}
