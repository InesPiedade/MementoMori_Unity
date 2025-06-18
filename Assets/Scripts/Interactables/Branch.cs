using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Branch : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] GameObject dog;

    [SerializeField] private UiManager uiManager;
    [SerializeField] private SaveController saveController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();

        if (collision)
        {
            Debug.Log("DOG");
            uiManager.ObjectiveDog();
            dog.SetActive(true);
            saveController.SaveGame();
        }
        else
        {
            uiManager.ObjectiveFlower();
            dog.SetActive(false);
        }
    }
}
