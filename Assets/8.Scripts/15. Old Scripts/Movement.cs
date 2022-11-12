using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public CharacterController controller;
    private Vector3 direction;
    public float speed = 8;

    public float jumpForce = 10;
    public float gravity = -10;
    
    public Transform groundCheck;
    public Transform vacioCheck;

    public LayerMask groundLayer;
    public LayerMask vacioLayer;

    public bool ableToMakeADoubleJump = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float hInput = Input.GetAxis("Horizontal");
        direction.x = hInput * speed;

        bool isGrounded = Physics.CheckSphere(groundCheck.position,0.2f, groundLayer);



        if (isGrounded)
        {
            ableToMakeADoubleJump = true;
            if (Input.GetButtonDown("Jump"))
            {
                direction.y = jumpForce;
            }
        } else
        {
            direction.y += gravity * Time.deltaTime;

            if (ableToMakeADoubleJump && Input.GetButtonDown("Jump"))
            {
                direction.y = jumpForce;
                ableToMakeADoubleJump = false;
            }
               
        }

        controller.Move(direction * Time.deltaTime);
    }
}
