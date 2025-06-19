using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour, IDamagable
{
    #region Declarations
    [Header("References")]

     private int health = 100;
    [SerializeField] private float horizontalInput;
     private float movementSpeed = 7;
    [SerializeField] private float jumpForce;
    [SerializeField] private float groundCheckDistance;
     private float interactDistance = 4;
    [SerializeField] private float footstepInterval = 7f;
    private float footstepTimer;
    private float stepInterval = 0.4f;


    private bool isInteracting;
    private bool isGrounded;
    private bool isWalking;
    private bool isRunning;
    private bool isSiting;
    private bool isPower = true;
    private bool isDead;
    [SerializeField] private bool branchBool;

    [SerializeField] private Vector2 boxSize;
    private Vector2 raycastDirection;



    [Header("Game Objects")]

    private Rigidbody2D rigidBody;
    public LayerMask groundLayer;
    public LayerMask UILayer;
    private SpriteRenderer spriteRenderer;

    private Animator animator;
    private UiManager uiManager;
    private InventoryController inventoryController;
    private Flower flowerScript;
    [SerializeField] private DarkVisionAbility darkVisionAbility;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private SaveController saveController;
    [SerializeField] private AudioClip jumpSoundClip;
    [SerializeField] private AudioClip damageSoundClip;
    [SerializeField] private AudioClip[] walkSoundClip;
    [SerializeField] private AudioClip runSoundClip;



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
        RigidBody.velocity = new Vector2(horizontalInput * movementSpeed, RigidBody.velocity.y);



        animator.SetFloat("MoveSpeed", Mathf.Abs(horizontalInput));
        animator.SetBool("IsGround", isGrounded);
        animator.SetBool("IsRunning", isRunning);

        Jump();
        Run();
        IsGround();
        InteractCheck();
        HandleFootSteps();

        if (Input.GetKeyDown(KeyCode.Q) && isPower == true)
        {

            if (isPower)
            {
                darkVisionAbility.Timer();
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
            animator.SetTrigger("Jump");
            SoundFXManager.instance.PlaySoundFXClip(jumpSoundClip, transform, 1f);
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
            SoundFXManager.instance.PlaySoundFXClip(runSoundClip, transform, 1f);
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isRunning = false;
            movementSpeed = 7;
        }
    }

    #endregion

    private void HandleFootSteps()
    {
        bool isWalking = Mathf.Abs(horizontalInput) > 0.1f && isGrounded && !isRunning;

        if(isWalking)
        {
            footstepTimer -= Time.deltaTime;

            if(footstepTimer <= 0f)
            {
                PlayRandomFootstep();
                footstepTimer = isRunning? stepInterval / 2 : stepInterval;
            }
        }
        else
        {
            footstepTimer = 0f; 
        }
    }


    private void PlayRandomFootstep()
    {
        if (walkSoundClip.Length > 0)
        {
            int randomIndex = Random.Range(0, walkSoundClip.Length);
            AudioClip clip = walkSoundClip[randomIndex];
            SoundFXManager.instance.PlaySoundFXClip(clip, transform, 1f);
        }
    }
    private void InteractCheck()
    {
        Debug.DrawRay(transform.position, raycastDirection * interactDistance, Color.green);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, raycastDirection, interactDistance, UILayer);

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
        animator.SetTrigger("Damage");
        SoundFXManager.instance.PlaySoundFXClip(damageSoundClip, transform, 1f);

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
        darkVisionAbility.Timer();
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
        darkVisionAbility.RecharginAbility();
        gameManager.VisionOff();
    }
}
