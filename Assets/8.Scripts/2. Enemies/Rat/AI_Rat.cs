using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

// Enemy states
public enum AI_State_R
{
    PATROLLING, CHASING, ATTACKING
}
public class AI_Rat : MonoBehaviour
{
    PlayerManager playerManager;

    [SerializeField] private BoxCollider patrolLimit;

    //AI_State variables

    [SerializeField] private AI_State_R currentAIState; //Saves the current enum value (current state)

    //Time between each attack
    [SerializeField] private float cooldownTime;

    //Determines if the player is in
    private bool canSeePlayer;

    //Tracks the cooldown time
    private float cooldownCounter;

    //Determines range of patrol
    [SerializeField] private float patrolRange;

    //Determines whether player should be able to trigger collider
    private bool playerInRangeY;
    private bool playerInRangeX;

    //Determines whether player is within a margin of error for being eligible for being attacked
    private bool playerInAttackRange;
    //Determines range of attack
    [SerializeField] private float attackRange;

    //Determine NPC speed
    [SerializeField] private float ratSpeed;

    //Values used for determining where the movement limit for this object instance will be
    //Tracks range of heights under which the player can trigger the collider
    [SerializeField] private float upperLimitX;
    [SerializeField] private float lowerLimitX;


    //Starting position values taken for reference
    [SerializeField] private float startX;
    [SerializeField] private float startY;
    [SerializeField] private float yOffset = 0.2f;

    [SerializeField] private bool movePositive; // Decides whether the snake moves left or right

    //Stores the animators
    [SerializeField] private Animator catAnim;
    [SerializeField] private Animator snakeAnim;

    void Awake()
    {
        snakeAnim = gameObject.GetComponentInChildren<Animator>();
        playerManager = GameObject.Find("Player").GetComponent<PlayerManager>();

        cooldownTime = 2f;
        cooldownCounter = 0f;
        patrolRange = (patrolLimit.size.x/2);
        ratSpeed = 4.5f;
        movePositive = true;

        startX = this.transform.position.x;
        startY = this.transform.position.y;

        upperLimitX = startX + patrolRange;
        lowerLimitX = startX - patrolRange;

        attackRange = 2.3f;
    }

    // Update is called once per frame
    void Update()
    {
        playerInRangeX = (lowerLimitX < playerManager.playerPosition.x) && (playerManager.playerPosition.x < upperLimitX);
        playerInRangeY = (startY - yOffset < playerManager.playerPosition.y) && (playerManager.playerPosition.y < startY + yOffset);
        playerInAttackRange = (playerManager.playerPosition.x - attackRange < this.transform.position.x) && (this.transform.position.x < playerManager.playerPosition.x + attackRange);

        enemyStates();
    }

    //method that tracks the AI_State
    void enemyStates()
    {
        //Create Switch to determine AI state
        switch (currentAIState)
        {
            case AI_State_R.PATROLLING:

                if (!playerInRangeX)
                {
                    //Will go forward until patrol limit
                    if (movePositive)
                    {
                        if (this.transform.position.x < upperLimitX)
                        {
                            turnToFace("right");
                            transform.Translate(Vector3.right * ratSpeed * Time.deltaTime);

                        }
                        else if (this.transform.position.x >= upperLimitX)
                        {
                            this.movePositive = false;
                        }
                    }
                    else if (!movePositive)
                    {
                        //Will go back until patrol limit
                        if (this.transform.position.x > lowerLimitX)
                        {
                            turnToFace("left");
                            transform.Translate(Vector3.right * ratSpeed * Time.deltaTime);
                        }
                        else if (this.transform.position.x <= lowerLimitX)
                        {
                            this.movePositive = true;
                        }
                    }
                }
                else
                {
                    if (!playerInAttackRange && playerInRangeX && playerInRangeY)
                    {
                        currentAIState = AI_State_R.CHASING;
                        Debug.Log("Chasing");
                    }
                    else if (playerInAttackRange && playerInRangeX && playerInRangeY)
                    {
                        currentAIState = AI_State_R.ATTACKING;
                        Debug.Log("Attacking");
                    }

                }

                break;

            case AI_State_R.CHASING:

                if (playerInRangeX)
                {
                    if (playerInRangeY)
                    {
                        //Chase until position matches
                        if (!playerInAttackRange)
                        {
                            if (playerManager.playerPosition.x < this.transform.position.x)
                            {
                                turnToFace("left");
                                transform.Translate(Vector3.right * ratSpeed * Time.deltaTime);
                            }
                            else
                            {
                                turnToFace("right");
                                transform.Translate(Vector3.right * ratSpeed * Time.deltaTime);
                            }
                        }

                        //Attack
                        else
                        {
                            currentAIState = AI_State_R.ATTACKING;
                            Debug.Log("Attacking");
                        }
                    }
                    else
                    {
                        currentAIState= AI_State_R.CHASING;
                        Debug.Log("Chasing");
                    }

                }
                else
                {
                    currentAIState = AI_State_R.PATROLLING;
                    Debug.Log("Patrolling");
                }

                break;

            case AI_State_R.ATTACKING:

                //Will attack while seeing the player
                if (canSeePlayer && playerInRangeX && playerInRangeY)
                {
                    //Attack player while the player is alive
                    if (!playerManager.playerDead)
                    {
                        //Keep attacking while in same position as player
                        if (playerInAttackRange)
                        {
                            //Make sure cooldown is on before attacking
                            if (this.cooldownCounter > 0)
                            {
                                this.cooldownCounter -= Time.deltaTime;
                            }
                            else //If cooldown is set
                            {
                                //Attacks player

                                //Damage changes based on power-up effect
                                if (playerManager.shielded)
                                {
                                    playerManager.TempHitPoints -= 1;

                                    //Animations
                                    catAnim.SetTrigger("Damaged");
                                    snakeAnim.SetTrigger("Attacking");
                                }
                                else
                                {
                                    playerManager.Lives -= 1;

                                    //Animations
                                    catAnim.SetTrigger("Damaged");
                                    snakeAnim.SetTrigger("Attacking");
                                }

                                this.cooldownCounter = cooldownTime;
                            }
                        }
                        //If player moves out of range chase
                        else
                        {
                            currentAIState = AI_State_R.CHASING;
                            Debug.Log("Chasing");
                        }

                    }
                    //If the player dies go back to patrolling
                    else
                    {
                        currentAIState = AI_State_R.PATROLLING;
                        Debug.Log("Patrolling");
                    }
                }
                //Patrol while player is out of line of sight
                else
                {
                    currentAIState = AI_State_R.PATROLLING;
                    Debug.Log("Patrolling");
                }

                break;
        }
    }

    private void turnToFace(string moveDirection)
    {
        if(moveDirection == "right")
        {
            this.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else if(moveDirection == "left")
        {
            this.transform.rotation = Quaternion.Euler(0f, -180f, 0f);
        }
    }

    //When facing right Vector3.right and Vector3.left apply normally
    //When facing left Vector3.right and Vector3.left flip
    //An easy equivalency is to use .right as "positive towards x direction" and .left as the inverse


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("PlayerEnteredTheArea");

            canSeePlayer = true;
            playerManager = other.gameObject.GetComponent<PlayerManager>();
            catAnim = other.gameObject.GetComponentInChildren<Animator>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("PlayerExitedTheArea");

            canSeePlayer = false;
        }
    }
}
