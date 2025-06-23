using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSave : MonoBehaviour
{
    [SerializeField] private SaveController saveController;
    [SerializeField] private UiManager uiManager;


    private void OnTriggerEnter2D(Collider2D startCollision)
    {
        Player player = startCollision.transform.GetComponent<Player>();

        if (startCollision)
        {
            MusicManager.instance.PlayForestMusic();
            MusicManager.instance.StopCaveMusic();
            MusicManager.instance.StopRunMusic();
            MusicManager.instance.StopMainMenuMusic();
            uiManager.ObjectiveFlower();
            saveController.SaveGame();
        }
    }
}
