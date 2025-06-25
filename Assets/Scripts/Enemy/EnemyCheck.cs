using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCheck : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private GameObject dog;

    private void OnTriggerEnter2D(Collider2D dogCollision)
    {
        Player player = dogCollision.gameObject.GetComponent<Player>();

        if (player != null)
        {
            Debug.Log("DOG");
            dog.SetActive(true);
        }
    }
}
