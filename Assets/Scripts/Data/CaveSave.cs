using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveSave : MonoBehaviour
{
    [SerializeField] private SaveController saveController;
    private UiManager uiManager;
    [SerializeField] private GameObject abilityTimer;

    private void Start()
    {
        uiManager = UiManager.instance;
    }

    private void OnTriggerEnter2D(Collider2D collisionCave)
    {
        Player player = collisionCave.transform.GetComponent<Player>();

        if (player != null)
        {
            MusicManager.instance.PlayCaveMusic();
            uiManager.ObjectiveCave();
            saveController.SaveGame();
            uiManager.QHint();
        	abilityTimer.SetActive(true);
        }
    }
}
