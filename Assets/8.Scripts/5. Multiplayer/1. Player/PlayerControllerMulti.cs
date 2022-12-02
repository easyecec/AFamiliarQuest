using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerControllerMulti : MonoBehaviour
{

    // Player values
    [SerializeField] private CharacterController controller;
    [SerializeField] private float speed = 8;
    [SerializeField] private float jumpForce = 10;
    [SerializeField] private float gravity = -20;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Animator catAnim;

    [SerializeField] private int terminalVelocity = -25;

    PlayerManager playerManager;

    private Vector3 direction;
    [SerializeField] private bool isGrounded;

    public bool doubleJump = false;

    //Camera values
    Camera playerCamera;
    [SerializeField] private float followSpeed = 2f;
    [SerializeField] private float yOffset = 0.3f;
    [SerializeField] private float xOffset = 7f;
    [SerializeField] private float zOffset = -10f;

    void Awake()
    {
        playerManager = gameObject.GetComponent<PlayerManager>();
        catAnim = gameObject.GetComponentInChildren<Animator>();
        playerCamera = Camera.main;
    }

    void Update()
    {
        if (!playerManager.playerDead)
        {
            MovePlayer();
            Jump();
        }

        if (direction.y <= terminalVelocity)
        {
            direction.y = terminalVelocity;
        }
    }

    void MovePlayer()
    {
        float hInput = Input.GetAxis("Horizontal");
        direction.x = hInput * speed;

        controller.Move(direction * Time.deltaTime);

        if (hInput < 0)
        {
            transform.rotation = Quaternion.Euler(0f, -180f, 0f);
            catAnim.SetBool("Walking", true);
        }
        else if (hInput > 0)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            catAnim.SetBool("Walking", true);
        }
        else
        {
            catAnim.SetBool("Walking", false);
        }
    }

    void Jump()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.2f, groundLayer);

        direction.y += gravity * Time.deltaTime;
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                direction.y = jumpForce;
                catAnim.SetTrigger("Jumping");
                doubleJump = true;
            }

            else
            {
                if (doubleJump)
                {
                    direction.y = jumpForce;
                    doubleJump = false;
                    catAnim.SetTrigger("Jumping");
                }
            }
        }
    }

    void CameraMovement()
    {
        Vector3 newPos = new Vector3(transform.position.x + xOffset, transform.position.y + yOffset, zOffset);
        playerCamera.transform.position = Vector3.Slerp(playerCamera.transform.position, newPos, followSpeed * Time.deltaTime);
    }

}
