using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerControllerType playerNumber;
    private Rigidbody2D playerRB;
    [SerializeField] private float fallSpeedLimit = 100f;
    [SerializeField] private float fallMultiplier = 1f;
    [SerializeField] private float speed = 20f;
    [SerializeField]
    [Range(0, 1)]
    private float speedMultiplier = 1f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float jumpMultiplier;
    [SerializeField] private int availableJumps = 1;
    [SerializeField] private int currentJumps;
    [SerializeField] private int availableDashes = 1;
    [SerializeField] private int currentDashes;
    [SerializeField] private float dashCooldown = 0.5f;
    [SerializeField] private float movement;
    private float dashTimer;
    private bool hasDashed;
    bool dashTrigger;
    bool jumpTrigger;
    Vector2 jumpVector;
    Vector2 moveVector;
    Vector2 dashVector;
    float isGroundedRayLength = 0.1f;
    float isWallHumpingRayLenght = 0.1f;
    [SerializeField] LayerMask layerMaskForGrounded;

    [SerializeField] private float dashForce = 20f;

    [SerializeField] private bool wallJumpEnabled;
    [SerializeField] private bool dashEnabled;
    [SerializeField] private GameObject damageArea;
    [SerializeField] private CapsuleCollider2D extraCollider;

    public int lives = 1;
    private Animator playerAnimator;
    [SerializeField]
    private GameObject tongue;
    private bool isGrounded
    {
        get
        {
            Vector2 position = transform.position;
            position.y = GetComponent<CapsuleCollider2D>().bounds.min.y + 0.1f;
            float length = isGroundedRayLength + 0.1f;
            Debug.DrawRay(position, Vector3.down * length);
            bool mainGrounded = Physics2D.Raycast(position, Vector3.down, length, layerMaskForGrounded.value);

            Vector2 extraPosition = extraCollider.gameObject.transform.position;
            extraPosition.y = extraCollider.bounds.min.y + 0.1f;
            Debug.DrawRay(extraPosition, Vector3.down * length);
            bool extraGrounded = Physics2D.Raycast(extraPosition, Vector3.down, length, layerMaskForGrounded.value);

            if (mainGrounded || extraGrounded)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    private bool isWallHumping
    {
        get
        {
            if (!wallJumpEnabled)
            {
                return false;
            }
            Vector2 position = transform.position;
            position.x = GetComponent<CapsuleCollider2D>().bounds.min.x + 0.1f;
            float length = isWallHumpingRayLenght + 0.1f;
            Debug.DrawRay(position, Vector3.left * length);
            Debug.DrawRay(position, Vector3.right * length);
            bool wallLeft = Physics2D.Raycast(position, Vector3.left, length, layerMaskForGrounded.value);
            bool wallRight = Physics2D.Raycast(position, Vector3.right, length, layerMaskForGrounded.value);

            if (wallLeft || wallRight)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }
    }

    private void Awake()
    {
        playerRB = this.GetComponent<Rigidbody2D>();
        playerAnimator = this.GetComponent<Animator>();
        currentDashes = availableDashes;
        currentJumps = availableJumps;
        lives = 1;
    }

    private void Update()
    {

        moveVector = playerRB.velocity;
        if (isGrounded)
        {
            currentJumps = availableJumps;
            
        }

        if (GameManager.Instance.hasGameStarted)
        {
            if (playerNumber == PlayerControllerType.Player1)
            {
                HandlePlayer1Controls();
            }

            if (playerNumber == PlayerControllerType.Player2)
            {
                HandlePlayer2Controls();
            }
        }
    }

    public void TakeDamage()
    {
        lives--;
        if (lives <= 0)
        {
            GameManager.Instance.RoundEnd(this);
        }
    }

    void HandlePlayer1Controls()
    {
        if (Input.GetKeyDown(KeyCode.W) && currentJumps > 0 && !jumpTrigger)
        {
            jumpVector.y = jumpForce;
            jumpTrigger = true;
            playerAnimator.SetTrigger("jump");
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            tongue.GetComponent<Animator>().SetTrigger("attack");
        }

        if (dashEnabled)
        {
            if (Input.GetKeyDown(KeyCode.Q) && !hasDashed && currentDashes > 0 && !dashTrigger && !jumpTrigger)
            {
                dashTrigger = true;
                hasDashed = true;
                dashTimer = dashCooldown;
                dashVector.y = dashForce;
            }

            if (hasDashed)
            {
                dashTimer -= 1 * Time.deltaTime;
                if (dashTimer < 0)
                {
                    hasDashed = false;
                    currentDashes = availableDashes;
                }
            }
        }
        movement = Input.GetAxis("Horizontal");
        moveVector.x = movement * speed * speedMultiplier;
    }

    void HandlePlayer2Controls()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && currentJumps > 0 && !jumpTrigger)
        {
            jumpVector.y = jumpForce;
            jumpTrigger = true;
            playerAnimator.SetTrigger("jump");
        }

        if (Input.GetKeyDown(KeyCode.RightControl))
        {
            tongue.GetComponent<Animator>().SetTrigger("attack2");
        }

        if (dashEnabled)
        {
            if (Input.GetKeyDown(KeyCode.LeftControl) && !hasDashed && currentDashes > 0 && !dashTrigger && !jumpTrigger)
            {
                dashTrigger = true;
                hasDashed = true;
                dashTimer = dashCooldown;
                dashVector.y = dashForce;
            }

            if (hasDashed)
            {
                dashTimer -= 1 * Time.deltaTime;
                if (dashTimer < 0)
                {
                    hasDashed = false;
                    currentDashes = availableDashes;
                }
            }
        }
        movement = Input.GetAxis("Horizontal2");
        moveVector.x = movement * speed * speedMultiplier;
    }

    private void FixedUpdate()
    {
        if (moveVector.y > fallSpeedLimit)
        {
            moveVector.y = fallSpeedLimit;
        }
        if (moveVector.y < fallSpeedLimit * -1) 
        {
            moveVector.y = fallSpeedLimit * -1;
        }



        if (jumpTrigger)
        {
           
            moveVector.y = jumpVector.y;
            //playerRB.AddForce(jumpVector * jumpForce * jumpMultiplier);
            currentJumps--;
            jumpTrigger = false;
        }
   
        if (dashTrigger && !jumpTrigger)
        {
            moveVector.y = dashVector.y;
            currentDashes--;
            dashTrigger = false;
        }

       

        playerRB.velocity = moveVector;

        if (!jumpTrigger && isWallHumping && moveVector.x != 0)
        {
            moveVector.y = 0;
            playerRB.velocity = moveVector;
        }
        else
        {
            if (playerRB.velocity.y < 0)
            {
                playerRB.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            }
        }

     
        
    }
}
