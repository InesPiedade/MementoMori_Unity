using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Altar : MonoBehaviour, IInteractable
{
    #region Declarations
    [Header("Game Objects")]
    [SerializeField] private GameObject flowerAltar;

    [Header("References")]
    private UiManager uiManager;
    private GameManager gameManager;
    [SerializeField] private Player player;


    #endregion

    private void Start()
    {
        uiManager = UiManager.instance;
    }
    public void Interact()
    {
        Debug.Log("ALTAR");

        StartCoroutine(SitAnimation());
        //Update inventory -flower
    }
    private IEnumerator SitAnimation()
    {
        player.Sit();
        flowerAltar.SetActive(true);
        yield return new WaitForSeconds(3f);
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
        yield return new WaitForSeconds(30f);
        uiManager.EndCutscene2();
        StartCoroutine(StartCutscene3());
    }

    private IEnumerator StartCutscene3()
    {
        yield return new WaitForSeconds(0.5f);
        //ui manager, credits
        uiManager.StartCutscene3();
        MusicManager.instance.PlayCreditsMusic();
        StartCoroutine(EndCutscene3());
    }
    private IEnumerator EndCutscene3()
    {
        yield return new WaitForSeconds(50f);
        MusicManager.instance.StopCreditsMusic();
        uiManager.EndCutscene3();
    }
}
