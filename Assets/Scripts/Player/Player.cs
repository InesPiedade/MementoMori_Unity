using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour, IDamagable
{
    #region Declarations
    [Header("References")]

    [SerializeField] private int health = 100;
    [SerializeField] private float horizontalInput;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private float interactDistace = 4;
    private bool isInteracting;

    [SerializeField] private Vector2 boxSize;
    private Vector2 raycastDirection;

    private bool isGrounded;

    [Header("Game Objects")]

    private Rigidbody2D rigidBody;
    public LayerMask groundLayer;
    public LayerMask interactLayer;
    private UiManager uiManager;
    private SpriteRenderer spriteRenderer;
    [SerializeField] GameObject branch;
    [SerializeField] GameObject dog;

    public Rigidbody2D RigidBody { get => rigidBody; set => rigidBody = value; }

    #endregion

    private void Awake()
    {
        RigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        uiManager = UiManager.instance;
    }

    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        RigidBody.velocity = new Vector2 (horizontalInput * movementSpeed, RigidBody.velocity.y);

        Jump();
        IsGround();
        InteractCheck();

        if (isInteracting)
        {
            horizontalInput = 0;
        }

        if(horizontalInput > 0)
        {
            spriteRenderer.flipX = true;
            raycastDirection = Vector2.right;
        }
        else if(horizontalInput < 0)
        {
            spriteRenderer.flipX = false;
            raycastDirection = Vector2.left;
        }
    }

    private void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && IsGround())
        {
            RigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void InteractCheck()
    {
        Debug.DrawRay(transform.position, raycastDirection * interactDistace, Color.green);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, raycastDirection, interactDistace, interactLayer);

        if (hit && Input.GetKeyDown(KeyCode.E))
        {
            hit.collider.gameObject.GetComponent<IInteractable>().Interact();
        }
    }

    public bool IsGround()
    {
        if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, groundCheckDistance, groundLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position -transform.up * groundCheckDistance, boxSize);
    }


    public void TakeDamage(int damage)
    {
        health -= damage;
        health = Mathf.Clamp(health, 0, 100);
        uiManager.HealthBar(health, 100);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject branch = collision.GetComponent<GameObject>();

        if(collision)
        {
            Debug.Log("A");
            dog.SetActive(true);
        }
        else
        {
            dog.SetActive(false);
        }
    }
}
