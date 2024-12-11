using System;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour{
    public CharacterController controller;

    public float speed = 20f;
    public float gravity = -9.81f;
    public float jumpForce = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.45f;
    public LayerMask groundMask;


    bool isGrounded;
    Vector3 velocity;
    void Start(){}

    void Update(){

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0){
            velocity.y = -0.5f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isGrounded){
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }  

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
        
}

