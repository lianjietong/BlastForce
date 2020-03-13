using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Movement - x-axis for side movement, z-axis for forwards and backwards
*/

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Animator animator; 
    //public Transform playerBody;
    
    public float speed = 12f;
    public float gravity = -9.81f;
    Vector3 velocity;
    public Transform groundCheck;
    public float groundDistance = 0.5f;  // sphere around groundcheck 
    public LayerMask groundMask;
    public float jumpHeight = 3f;
    public float rotationSpeed = 1;
    bool isGrounded;

    public Camera cam;
    Vector2 mousePos;

    // Update is called once per frame
    void Update()
    {

        Move();
    }

    public void Move(){
        
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if(isGrounded && velocity.y < 0){
            velocity.y = -2f;
        }

        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 rotation = new Vector3(0f, z * rotationSpeed * Time.deltaTime, 0f);
        Vector3 move = new Vector3(x, 0f, z);                               //global coords movement
        transform.Rotate(rotation*2);
        
        controller.Move(move * speed * Time.deltaTime);
        
        animator.SetFloat("Speed", (x + z));

        if(Input.GetButtonDown("Jump") && isGrounded){
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            animator.SetBool("isJumping", true);
        }else{
            isGrounded = true;
            animator.SetBool("isJumping", false);
        }

        // if jumping and shooting down rocket jump
            // jump double the height

        if(Input.GetButtonDown("Jump") && Input.GetButtonDown("Fire1")){
            // double jump?
            // get angle of shot?
            
        }
        velocity.y += (gravity * 7) * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

    }
}

