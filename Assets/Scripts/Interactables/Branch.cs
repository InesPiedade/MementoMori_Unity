using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Branch : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] GameObject dog;

    [SerializeField] private UiManager uiManager;
    [SerializeField] private SaveController saveController;

    private void OnTriggerEnter2D(Collider2D Branchcollision)
    {
        Player player = Branchcollision.gameObject.GetComponent<Player>();

        if (Branchcollision)
        {
            Debug.Log("DOG");
            MusicManager.instance.PlayRunMusic();
            MusicManager.instance.StopForestMusic();
            uiManager.ObjectiveDog();
            dog.SetActive(true);
            saveController.SaveGame();
        }
        else
        {
            MusicManager.instance.StopRunMusic();
            MusicManager.instance.PlayForestMusic();
            uiManager.ObjectiveFlower();
            dog.SetActive(false);
        }
    }
}
