using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveSave3 : MonoBehaviour
{
    [SerializeField] private SaveController saveController;
    [SerializeField] private UiManager uiManager;
    [SerializeField] private GameObject abilityTimer;


    private void OnTriggerEnter2D(Collider2D collisionCave)
    {
        Player player = collisionCave.transform.GetComponent<Player>();

        if (collisionCave)
        {
            uiManager.ObjectiveAltar();
            saveController.SaveGame();
            abilityTimer.SetActive(true);
        }
    }
}
