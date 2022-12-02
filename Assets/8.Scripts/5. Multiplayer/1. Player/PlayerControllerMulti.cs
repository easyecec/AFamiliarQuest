using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerControllerMulti : NetworkBehaviour
{

    // Player values
    [SerializeField] private NetworkCharacterControllerPrototype controller;
    [SerializeField] private float speed = -10;
    [SerializeField] private float jumpForce = 10;
    [SerializeField] private float gravity = -20;
    [SerializeField] private GameObject catSprite;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Animator catAnim;

    [SerializeField] private int terminalVelocity = -25;

    PlayerManagerMulty playerManager;

    private Vector3 direction;
    [SerializeField] private bool isGrounded;

    public bool doubleJump = false;

    //Camera values
    Camera playerCamera;
    [SerializeField] private float followSpeed = 2.5f;
    [SerializeField] private float yOffset = 1f;
    [SerializeField] private float xOffset = -14f;
    [SerializeField] private float zOffset = 0.7f;

    void Awake()
    {
        catSprite.transform.rotation = Quaternion.Euler(0f, 90f, 0f);
        controller = GetComponent<NetworkCharacterControllerPrototype>();
        playerManager = gameObject.GetComponent<PlayerManagerMulty>();
        catAnim = gameObject.GetComponentInChildren<Animator>();
        playerCamera = Camera.main;
    }

    void Update()
    {
        if (!playerManager.PlayerDead)
        {
            //MovePlayer();
            //Jump();
            CameraMovement();
        }

        if (direction.y <= terminalVelocity)
        {
            direction.y = terminalVelocity;
        }
    }

    public override void FixedUpdateNetwork()
    {
        if(GetInput(out NetworkInputData data))
        {
            direction.z = data.Direction.z * speed;

            controller.Move(direction * Time.deltaTime * 50);

            if (data.Direction.z < 0)
            {
                catSprite.transform.rotation = Quaternion.Euler(0f, -90f, 0f);
                catAnim.SetBool("Walking", true);
            }
            else if (data.Direction.z > 0)
            {
                catSprite.transform.rotation = Quaternion.Euler(0f, 90f, 0f);
                catAnim.SetBool("Walking", true);
            }
            else
            {
                catAnim.SetBool("Walking", false);
            }
        }
    }

    void MovePlayer()
    {
        /*
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
        */
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
        Vector3 newPos = new Vector3(xOffset, this.transform.position.y + yOffset, this.transform.position.z + zOffset);
        playerCamera.transform.position = Vector3.Slerp(playerCamera.transform.position, newPos, followSpeed * Time.deltaTime);
    }

}
