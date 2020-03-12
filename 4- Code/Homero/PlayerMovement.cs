using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Animator animator; 
    //public Transform playerBody;
    
    public float speed = 12f;
    public float gravity = -9.81f;
    Vector3 velocity;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public float jumpHeight = 3f;
    public float rotationSpeed = 600;
    bool isGrounded;

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0){
            velocity.y = -2f;
        }
        
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        //Vector3 move = transform.right * x + transform.forward * z;
        //transform.localRotation = Quaternion.Euler(x, 0f , 0f);
        Vector3 rotation = new Vector3(0f, x * rotationSpeed * Time.deltaTime, 0f);
        //Vector3 move = new Vector3(x, 0f, z);   //contantly moves and rotates character
        Vector3 move = new Vector3(0f, 0f, z);
        move = transform.TransformDirection(move);
        transform.Rotate(rotation);

        controller.Move(move * speed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isGrounded){
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
            animator.SetBool("isJumping", true);
        }else if(isGrounded){
            animator.SetBool("isJumping", false);
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        animator.SetFloat("Speed", (x + z));


        //playerBody.Rotate(Vector3.up * move);
    }

/*
    public void onLanding(){
        animator.SetBool("isJumping", false);
    }*/
}


/*
    might be usefule to rotate
    transform.Rotate(0f, 180f, 0f)
*/