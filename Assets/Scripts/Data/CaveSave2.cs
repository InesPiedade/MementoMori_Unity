using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveSave2 : MonoBehaviour
{
    [SerializeField] private SaveController saveController;
    private GameManager gameManager;
    private UiManager uiManager;

    private void Start()
    {
        uiManager = UiManager.instance;
    }


    private void OnTriggerEnter2D(Collider2D collisionCave)
    {
        Player player = collisionCave.transform.GetComponent<Player>();

        if (collisionCave)
        {
            uiManager.ObjectiveCave();
            saveController.SaveGame();
        }
    }
}
