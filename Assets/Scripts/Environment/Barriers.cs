using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barriers : MonoBehaviour
{
    public int damageOnContact = 200;

    private void OnCollisionEnter2D(Collision2D barrierCollision)
    {
        Player player = barrierCollision.gameObject.GetComponent<Player>();

        if (player != null)
        {
            player.TakeDamage(damageOnContact);
        }
    }
}
