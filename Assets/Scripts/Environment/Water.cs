using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D waterCollision)
    {
        Player player = waterCollision.gameObject.GetComponent<Player>();

        if (player != null)
        {
            player.TakeDamage(5);
        }
    }
}
