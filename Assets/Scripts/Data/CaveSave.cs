using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveSave : MonoBehaviour
{
    [SerializeField] private SaveController saveController;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.transform.GetComponent<Player>();

        if (collision)
        {
            saveController.SaveGame();
        }
    }
}
