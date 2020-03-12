using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // references
    public CharacterController controller;
    public Animator animator;

    // declare and initialize needed variables
    public float movementSpeed = 10f;
    public float gravity = -9.81f;
    public float gravityScale = 3f;
    public float crouchScale = 0.5f;
    public float jumpForce = 20f;
    public int rocketJumps = 2;

    private int currentDirection = 1;
    private float crouchUpwardsDistance = 0f;
    private bool isCrouching = false;
    private bool canJump = true;
    private bool canRocketJump = false;
    private int performedJumps = 0;

    // user input movement
    private float horizontalMovement;
    private float verticalMovement;

    // 3-d direction movement vector
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
        horizontalMovement = Input.GetAxis("Horizontal");
        verticalMovement = Input.GetAxis("Vertical");
    }

    /* Fixed Update can run once, zero, or several times per frame,
     * depending on how many physics frames per second are set in the
     * time settings, and how fast/slow the framerate is
     */
    void FixedUpdate()
    {
        // rotate left or right
        RotateDirection();

        // handle jumping
        Jump();

        // handle crouching
        Crouch();

        // apply animations
        UpdateAnimation();

        // handle horizontalMovement
        moveDirection.x = horizontalMovement * movementSpeed;

        // always apply gravity effect
        moveDirection.y += gravity * gravityScale * Time.deltaTime;

        // move controller
        controller.Move(moveDirection * Time.deltaTime);
    }

    void RotateDirection()
    {
        // rotate to the right
        if ((Input.GetKey(KeyCode.D) | Input.GetKey(KeyCode.RightArrow)) && (currentDirection != 1))
        {
            currentDirection = 1;
            transform.rotation = Quaternion.Euler(0, 90, 0);
        }

        // rotate to the left
        if ((Input.GetKey(KeyCode.A) | Input.GetKey(KeyCode.LeftArrow)) && (currentDirection != -1))
        {
            currentDirection = -1;
            transform.rotation = Quaternion.Euler(0, -90, 0);
        }
    }

    void Jump()
    {
        // check if player is on ground
        if (controller.isGrounded)
        {
            // reset number or jumps
            performedJumps = 0;
            moveDirection.y = 0f;

            // disable jumping animation when not jumping
            animator.SetBool("isJumping", false);

            // first jump
            if (Input.GetButtonDown("Jump") && canJump)
            {
                animator.SetBool("isJumping", true);
                canRocketJump = false;
                performedJumps += 1;
                moveDirection.y = jumpForce;
                //controller.Move(moveDirection * Time.deltaTime);
            }
        }

        // multiple jumps
        if (!controller.isGrounded && (performedJumps < rocketJumps))
        {
            if (Input.GetButtonDown("Jump") && canRocketJump)
            {
                animator.SetBool("isJumping", true);
                canRocketJump = false;
                performedJumps += 1;
                moveDirection.y += jumpForce * 1.25f;
            }
            else
            {
                canRocketJump = true;
            }
        }
    }

    void Crouch()
    {
        // crouch using the letter "C"
        if (Input.GetKey(KeyCode.C))
        {
            gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
            gameObject.transform.localScale *= crouchScale;
            isCrouching = true;

            // disable jumping if something above character while crouching
            if (crouchUpwardsDistance > 1.7f)
            {
                canJump = false;
            }
            else
            {
                crouchUpwardsDistance = 0f;
                canJump = true;
            }
        }
        else
        {
            if (crouchUpwardsDistance <= 1.7f)
            {
                gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
                isCrouching = false;
                crouchUpwardsDistance = 0f;
                canJump = true;
            }
        }

        // return to original height only when possible
        if (isCrouching)
        {
            // shoot a ray upwards and detect any collisions
            Ray ray = new Ray(transform.position, Vector3.up * 2f);
            RaycastHit[] hits = Physics.RaycastAll(ray, 2f);
            crouchUpwardsDistance = Vector3.Distance(transform.position, hits[0].point);

            // These lines of code are for debugging purposes only
            /*
            Debug.DrawRay(transform.position, Vector3.up * 2f, Color.red);
            Debug.DrawLine(hits[0].point, hits[0].point + Vector3.left * 5, Color.green);
            Debug.Log(crouchUpwardsDistance);
            */

        }
    }

    void Shooting()
    {

    }

    void UpdateAnimation()
    {
        // sprinting animation (both right and left)
        if (Input.GetKey(KeyCode.D) | Input.GetKey(KeyCode.RightArrow) |
            Input.GetKey(KeyCode.A) | Input.GetKey(KeyCode.LeftArrow)  &&
            !Input.GetKey("space"))
        {
            animator.SetBool("isSprinting", true);
        }
        else
        {
            animator.SetBool("isSprinting", false);
        }
    }
}
