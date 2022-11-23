using System.Collections;
using System.Collections.Generic;
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

    [SerializeField] private Transform Target;

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
    private float attackRange;

    //Determine NPC speed
    [SerializeField] private float ratSpeed;

    //Values used for determining where the movement limit for this object instance will be
    //Tracks range of heights under which the player can trigger the collider
    [SerializeField] private float upperLimitX;
    [SerializeField] private float lowerLimitX;


    //Starting position values taken for reference
    [SerializeField] private float startX;
    [SerializeField] private float startY;
    [SerializeField] private float startZ;

    [SerializeField] private bool movePositive; // Decides whether the fairy moves up or down

    //Stores the player animator
    [SerializeField] private Animator catAnim;

    void Awake()
    {
        playerManager = GameObject.Find("Player").GetComponent<PlayerManager>();

        cooldownTime = 2f;
        cooldownCounter = 0f;
        patrolRange = 4.55f;
        ratSpeed = 2.85f;
        movePositive = true;

        startX = this.transform.position.x;
        startY = this.transform.position.y;
        startZ = this.transform.position.z;

        upperLimitX = startX + patrolRange;
        lowerLimitX = startX - patrolRange;

        attackRange = 0.025f;
    }

    // Update is called once per frame
    void Update()
    {
        playerInRangeX = (lowerLimitX < playerManager.playerPosition.x) && (playerManager.playerPosition.x < upperLimitX);
        playerInRangeY = (startY - 0.2 < playerManager.playerPosition.y) && (playerManager.playerPosition.y < startY+0.2);
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
                            transform.Translate(Vector3.left * ratSpeed * Time.deltaTime);
                        }
                        else if (this.transform.position.x <= lowerLimitX)
                        {
                            this.movePositive = true;
                        }
                    }
                    Debug.Log("Patrolling");
                }
                else
                {
                    if (!playerInAttackRange && playerInRangeX && playerInRangeY)
                    {
                        currentAIState = AI_State_R.CHASING;
                    }
                    else if (playerInAttackRange && playerInRangeX && playerInRangeY)
                    {
                        currentAIState = AI_State_R.ATTACKING;
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
                                transform.Translate(Vector3.left * ratSpeed * Time.deltaTime);
                            }
                            else
                            {
                                transform.Translate(Vector3.right * ratSpeed * Time.deltaTime);
                            }
                        }

                        //Attack
                        else
                        {
                            currentAIState = AI_State_R.ATTACKING;
                        }
                    }
                    else
                    {
                        currentAIState= AI_State_R.CHASING;
                    }

                }
                else
                {
                    currentAIState = AI_State_R.PATROLLING;
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
                                Debug.Log("Attacking");

                                //Damage changes based on power-up effect
                                if (playerManager.shielded)
                                {
                                    playerManager.tempHitPoints -= 1;
                                    catAnim.SetTrigger("Damaged");
                                }
                                else
                                {
                                    playerManager.lives -= 1;
                                    catAnim.SetTrigger("Damaged");
                                }

                                this.cooldownCounter = cooldownTime;
                            }
                        }
                        //If player moves out of range chase
                        else
                        {
                            currentAIState = AI_State_R.CHASING;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            canSeePlayer = true;
            catAnim = other.gameObject.GetComponentInChildren<Animator>();
            Debug.Log("PlayerEnteredTheArea");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            canSeePlayer = false;
            Debug.Log("PlayerExitedTheArea");
        }
    }
}
