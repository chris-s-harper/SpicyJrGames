using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    //editable fields to adjust movement speed, grounddetectionlocation,etc
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float jumpVelocity;
    [SerializeField]
    private Transform groundDetectPoint;
    [SerializeField]
    private LayerMask whatIsGround;
    [SerializeField]
    private float groundDetectRadius = 0.2f;
    [SerializeField]
    private float disappearColorAlphaValue;
    [SerializeField]
    private float disappearSpeed;


    private Rigidbody2D playerRigidBody;

    private bool canControl = false;
    private bool levelComplete = false;
    private bool facingRight;
    private bool isOnGround;
    private string currentColor;
    private SpriteRenderer playerSprite;
    private AudioSource jumpSound;
    private Animator animator;
    private Checkpoint activeCheckpoint;
    private Player playerSelf;

    public Vector2 RespawnTransform = new Vector2();

	// Use this for initialization
	void Start ()
    {
        //sets first respawn location to be starting location.
        RespawnTransform = gameObject.transform.position;
        facingRight = true;
        Respawn();
        //gets necessary components of the audio, rigidbody, spriterenders
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerSprite = GetComponent<SpriteRenderer>();
        jumpSound = GetComponent<AudioSource>();
        //playerSprite.color = pink;
        currentColor = "pink";
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        
	}

    void FixedUpdate()
    {
        if (canControl)
        {
            handleMovement();
            handleJumping();
            updateIsOnGround();
            respawnOnCommand();
        }
        if (levelComplete)
        {
            fadeOut();
        }
    }

    void handleMovement()
    {
        double movingRightInput = 0.01;
        double movingLeftInput = -0.01;

        float movementInput = Input.GetAxis("Horizontal");
        float currentYVelocity = playerRigidBody.velocity.y;

        //AnimationHandler(movementInput);
        animator.SetFloat("Speed", Math.Abs(movementInput));

        Vector2 velocityToSet = new Vector2(moveSpeed * movementInput, currentYVelocity);

        playerRigidBody.velocity = velocityToSet;

        if (movementInput > 0 && !facingRight)
        {
            Flip();
        }
        else if (movementInput < 0 && facingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        //used for fliping character sprite
        facingRight = !facingRight;
        int flipX = -1;
        float playerScaleXWhenFacingLeft = ((transform.localScale.x) * flipX);
        Vector3 theScale = new Vector3(playerScaleXWhenFacingLeft, transform.localScale.y, transform.localScale.z);
        transform.localScale = theScale;
    }

    //handles the jump input.
    private void handleJumping()
    {
        //only if the jump button is pressed, and the player is on the ground.
        if (Input.GetButtonDown("Jump") && isOnGround)
        {

            jumpSound.Play();

            float currentXVelocity = playerRigidBody.velocity.x;

            Vector2 velocityToSet = new Vector2(currentXVelocity, jumpVelocity);

            playerRigidBody.velocity = velocityToSet;
        }
    }

    //updates to see if the player is on ground so they can jump, but not jump indefinitely.
    private void updateIsOnGround()
    {
        Collider2D[] groundObjects = Physics2D.OverlapCircleAll(groundDetectPoint.position, groundDetectRadius, whatIsGround);

        isOnGround = (groundObjects.Length > 0);

        animator.SetBool("IsOnGround", isOnGround);
    }

    private void respawnOnCommand()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Respawn();
        }
    }

    private Checkpoint checkActiveCheckpoint()
    {
        return activeCheckpoint;
    }

    private void fadeOut()
    {
        float alphaValue = Mathf.Lerp(playerSprite.color.a, disappearColorAlphaValue, Time.deltaTime * disappearSpeed);
        playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, alphaValue);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "checkpoint")
        {
            activeCheckpoint = collision.gameObject.GetComponent<Checkpoint>();
        }
    }

    public void Respawn()
    {
        checkActiveCheckpoint();
        //audioSource.Play();
        if (activeCheckpoint != null)
        {
            activeCheckpoint.ChangeRespawnToSet(this);
        }
        transform.position = RespawnTransform;
    }

    //changes the player sprite to match the "safe" blocks
    public void ChangeToGreen()
    {
        animator.SetBool("IsGreen", true);
        currentColor = "green";
    }

    public void ChangeToPink()
    {
        animator.SetBool("IsGreen", false);
        currentColor = "pink";
    }

    //public function used by blocks to check if the player can walk on them.
    public string CheckColor()
    {
        if (currentColor == "pink")
        {
            return "pink";
        }

        if (currentColor == "green")
        {
            return "green";
        }

        //debug statement if neither of the available colors
        else
        {
            Debug.Log("Error, player is either both red and blue, or neither");
            return "Error";
        }
    }

    public void ChangeCanControl(bool boolToSet)
    {
        canControl = boolToSet;
    }

    public void ChangeIsLevelComplete(bool boolToSet)
    {
        levelComplete = boolToSet;
    }
}
