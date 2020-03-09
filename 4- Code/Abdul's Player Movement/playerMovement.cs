using UnityEngine;

public class playerMovement : MonoBehaviour
{
    // references
    public CharacterController controller;
    public Animator animator;

    // declare and initialize needed variables
    public float speed = 6f;
    public float gravity = 20f;
    public float jump = 15f;

    float horizontal;
    float vertical;

    Vector3 moveDirection = Vector3.zero;


    // Start is called before the first frame update
    void Start()
    {
        // start up the controller and animator objects
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // setup horizontal and vertical inputs (Note: vertical is then used for z-axis if we change camera view)
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        // apply animations
        UpdateAnimation();
    }

    /* Fixed Update can run once, zero, or several times per frame,
     * depending on how many physics frames per second are set in the
     * time settings, and how fast/slow the framerate is
     */
    void FixedUpdate()
    {
        // allow to move horizontally and jump while player isGrounded
        if (controller.isGrounded)
        {
            // disable jumping animation when not jumping
            animator.SetBool("isJumping", false);

            // move horizontally when on ground
            moveDirection = new Vector3(horizontal, 0f, 0f);
            moveDirection *= speed;
            controller.Move(moveDirection * Time.deltaTime);

            // handle jumping
            Jump();
        }

        // handle crouching
        Crouch();

        // always apply gravity effect
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }

    void Crouch() {
        // crouch using the down arrow or s
        if (Input.GetKey(KeyCode.S) | Input.GetKey(KeyCode.DownArrow))
        {
            gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
        else
        {
            gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    void Jump()
    {
        // jump or rocket jump
        if (Input.GetKey(KeyCode.W) | Input.GetKey(KeyCode.UpArrow))
        {
            animator.SetBool("isJumping", true);
            moveDirection.y = jump;
        }
        if (Input.GetKey("space"))
        {
            animator.SetBool("isJumping", true);
            moveDirection.y = jump * 2;
        }
    }

    void UpdateAnimation()
    {
        // sprinting (moving right)
        if (Input.GetKey(KeyCode.D) | Input.GetKey(KeyCode.RightArrow) && !(Input.GetKey("space") | Input.GetKey(KeyCode.W) | Input.GetKey(KeyCode.UpArrow)))
        {
            animator.SetBool("isSprinting", true);
        }
        else
        {
            animator.SetBool("isSprinting", false);
        }

        // backwards Running (moving left)
        if (Input.GetKey(KeyCode.A) | Input.GetKey(KeyCode.LeftArrow) && !(Input.GetKey("space") | Input.GetKey(KeyCode.W) | Input.GetKey(KeyCode.UpArrow)))
        {
            animator.SetBool("isBackwardsRunning", true);
        }
        else
        {
            animator.SetBool("isBackwardsRunning", false);
        }
    }
}
