using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Altar : MonoBehaviour, IInteractable
{
    #region Declarations
    [Header("Game Objects")]
    [SerializeField] private GameObject cutscene2;
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
        Debug.Log("ALTAR");

        StartCoroutine(SitAnimation());
        //saveController.SaveGame();
        //Update inventory 
    }
    private IEnumerator SitAnimation()
    {
        //animator sit 
        yield return new WaitForSeconds(2f);
        StartCoroutine(StartCutscene2());
    }

    private IEnumerator StartCutscene2()
    {
        yield return new WaitForSeconds(0.5f);
        uiManager.StartCutscene2();
        StartCoroutine(EndCutscene2());
    }
    private IEnumerator EndCutscene2()
    {
        yield return new WaitForSeconds(4f);
        uiManager.EndCutscene2();
        //ui manager, credits

    }
}
