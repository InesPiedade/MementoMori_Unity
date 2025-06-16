using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour, IDamagable
{
    #region Declarations
    [Header("References")]

    [SerializeField] private int health = 100;
    [SerializeField] private float horizontalInput;
    [SerializeField] private float movementSpeed = 7;
    [SerializeField] private float jumpForce;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private float interactDistance = 4;

    private bool isInteracting;
    private bool isGrounded;
    private bool isWalking;
    private bool isRunning;
    private bool isSiting;
    private bool isPower = true;
    private bool isDead;

    [SerializeField] private Vector2 boxSize;
    private Vector2 raycastDirection;



    [Header("Game Objects")]

    private Rigidbody2D rigidBody;
    public LayerMask groundLayer;
    public LayerMask interactLayer;
    private SpriteRenderer spriteRenderer;
    [SerializeField] GameObject branch;
    [SerializeField] GameObject dog;

    private Animator animator;
    private UiManager uiManager;
    private InventoryController inventoryController;
    private Flower flowerScript;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private SaveController saveController;


    public Rigidbody2D RigidBody { get => rigidBody; set => rigidBody = value; }

    #endregion

    private void Awake()
    {
        RigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        uiManager = UiManager.instance;
        inventoryController = FindAnyObjectByType<InventoryController>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        animator.SetFloat("MoveSpeed", Mathf.Abs(horizontalInput));
        animator.SetBool("IsGround", isGrounded);
        animator.SetBool("IsRunning", isRunning);

        RigidBody.velocity = new Vector2(horizontalInput * movementSpeed, RigidBody.velocity.y);

        Jump();
        Run();
        IsGround();
        InteractCheck();

        if (Input.GetKeyDown(KeyCode.Q) && isPower == true)
        {

            if (isPower)
            {
                DarkVisionOn();
                StartCoroutine(VisionCooldown());
            }
            else
            {
                DarkVisionOff();
            }

        }

        if (isInteracting)
        {
            horizontalInput = 0;
        }

        if (horizontalInput < 0)
        {
            spriteRenderer.flipX = true;
            raycastDirection = Vector2.left;
        }
        else if (horizontalInput > 0)
        {
            spriteRenderer.flipX = false;
            raycastDirection = Vector2.right;
        }
    }

    #region Actions
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            RigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    public void Sit()
    {
        //if(Input.GetKeyDown(KeyCode.E))
        animator.SetTrigger("Sit");
    }

    private void Run()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded)
        {
            isRunning = true;
            movementSpeed = 15;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isRunning = false;
            movementSpeed = 7;
        }
    }

    #endregion

    private void InteractCheck()
    {
        Debug.DrawRay(transform.position, raycastDirection * interactDistance, Color.green);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, raycastDirection, interactDistance, interactLayer);

        if (hit.collider != null && Input.GetKeyDown(KeyCode.E) && hit.collider.CompareTag("Item"))
        {           
            Item item = hit.collider.GetComponent<Item>();

            if (item != null)
            {
                Debug.Log("Item Added");
                //add to inventory
                bool itemAdded = inventoryController.AddItem(hit.collider.gameObject);

                if (itemAdded)
                {
                    IInteractable interactable = hit.collider.GetComponent<IInteractable>();
                    if (interactable != null)
                    {
                        interactable.Interact();
                    }
                }
            }
        }
    }

    public bool IsGround()
    {
        if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, groundCheckDistance, groundLayer))
        {
            isGrounded = true;
            return true;
        }
        else
        {
            isGrounded = false;
            return false;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * groundCheckDistance, boxSize);
    }


    public void TakeDamage(int damage)
    {
        health -= damage;
        health = Mathf.Clamp(health, 0, 100);
        uiManager.HealthBar(health, 100);

        if (health <= 0 && !isDead)
        {
            isDead = true;
            StartCoroutine(Death());
        }
        else
        {
            isDead = false;
        }
    }

    private IEnumerator Death()
    {
        //animator.SetTrigger("Death");

        yield return new WaitForSeconds(0.5f);
        uiManager.GameOver();
        Time.timeScale = 0f;
    }

    public void DarkVisionOn()
    {
        gameManager.VisionOn();
        StartCoroutine(TimerDarkVision());
    }

    private IEnumerator TimerDarkVision()
    {
        yield return new WaitForSeconds(5f);
        DarkVisionOff();
    }

    private IEnumerator VisionCooldown()
    {
        isPower = false;
        yield return new WaitForSeconds(10f);
        isPower = true;
    }

    public void DarkVisionOff()
    {
        gameManager.VisionOff();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject branch = collision.GetComponent<GameObject>();

        if (collision)
        {
            Debug.Log("DOG");
            dog.SetActive(true);
            saveController.SaveGame();
        }
        else
        {
            dog.SetActive(false);
        }

        //if (collision.CompareTag("Item"))
        //{
        //    Item item = collision.GetComponent<Item>();
        //    if (item != null)
        //    {
        //        bool itemAdded = inventoryController.AddItem(collision.gameObject);
        //        if (itemAdded)
        //        {
        //            gameObject.GetComponent<IInteractable>().Interact();
        //        }
        //    }
        //}
    }
}
