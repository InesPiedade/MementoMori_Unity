using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour
{

    #region Declarations
    [Header("References")]
    [SerializeField] private float minimumDistance;
    [SerializeField] private float speed;

    [Header("Game Objects")]

    [SerializeField] private Transform target;
  
    private Rigidbody2D rb;

    #endregion

    private void Start()
    {
        target = GameObject.Find("Player").transform;
        gameObject.SetActive(false);
    }

    private void Update()
    {
        float distance = Vector2.Distance(target.position, transform.position);

        if(distance <= minimumDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            transform.right = target.position - transform.position;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player player = collision.transform.GetComponent<Player>();
        if (player != null)
        {
            player.TakeDamage(20);
        }
    }
}
