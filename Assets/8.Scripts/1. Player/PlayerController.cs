using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private float speed = 8;
    [SerializeField] private float jumpForce = 10;
    [SerializeField] private float gravity = -20;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Animator catAnim;

    PlayerManager playerManager;

    private Vector3 direction;
    private bool isGrounded;
    private bool jumping;

    public bool doubleJump = false;

    void Awake()
    {
        playerManager = GameObject.Find("Player").GetComponent<PlayerManager>();

        catAnim = gameObject.GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (!playerManager.playerDead)
        {
            MovePlayer();
            Jump();
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
                catAnim.SetBool("Jumping", true);
                doubleJump = true;
            }

            else
            {
                if (doubleJump)
                {
                    direction.y = jumpForce;
                    doubleJump = false;
                }

                else
                {
                    catAnim.SetBool("Jumping", false);
                }
            }
        }
    }

}
