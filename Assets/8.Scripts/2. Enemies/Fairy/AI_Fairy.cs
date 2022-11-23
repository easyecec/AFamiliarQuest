using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Enemy states
public enum AI_State_F
{
    PATROLLING, CHASING, ATTACKING
}

//[RequireComponent(typeof(NavMeshAgent))]
public class AI_Fairy : MonoBehaviour
{
    PlayerManager playerManager;

    [SerializeField] private Transform Target;

    //AI_State variables

    [SerializeField] private AI_State_F currentAIState; //Saves the current enum value (current state)

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
    [SerializeField] private float fairySpeed;

    //Values used for determining where the movement limit for this object instance will be
    //Tracks range of heights under which the player can trigger the collider
    [SerializeField] private float upperLimitY;
    [SerializeField] private float lowerLimitY;


    //Starting position values taken for reference
    [SerializeField] private float startX;
    [SerializeField] private float startY;
    [SerializeField] private float startZ;

    [SerializeField] private bool movePositive; // Decides whether the fairy moves up or down


    void Awake()
    {
        playerManager = GameObject.Find("Player").GetComponent<PlayerManager>();

        cooldownTime = 2f;
        cooldownCounter = 0f;
        patrolRange = 4.55f;
        fairySpeed = 3.2f;
        movePositive = true;

        startY = this.transform.position.y;
        startX= this.transform.position.x;
        startZ= this.transform.position.z;

        upperLimitY = startY + patrolRange;
        lowerLimitY = startY - patrolRange;

        attackRange = 0.025f;
        
        //Agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        playerInRangeX = (startX - patrolRange-0.5 < playerManager.playerPosition.x) && (playerManager.playerPosition.x < startX + patrolRange);
        playerInRangeY = (lowerLimitY < playerManager.playerPosition.y) && (playerManager.playerPosition.y < upperLimitY);
        playerInAttackRange = (playerManager.playerPosition.y - attackRange < this.transform.position.y) && (this.transform.position.y < playerManager.playerPosition.y + attackRange);
        enemyStates();
    }


    //method that tracks the AI_State
    void enemyStates()
    {
        //Create Switch to determine AI state
        switch (currentAIState)
        {
            case AI_State_F.PATROLLING:

                if (!playerInRangeY || !playerInRangeX)
                {
                    //Will go up until patrol limit
                    if (movePositive)
                    {
                        if (this.transform.position.y < upperLimitY)
                        {
                            
                            transform.Translate(Vector3.up * fairySpeed * Time.deltaTime);

                        }
                        else if (this.transform.position.y >= upperLimitY)
                        {
                            this.movePositive = false;
                        }
                    }
                    else if (!movePositive)
                    {
                        //Will go down until patrol limit
                        if (this.transform.position.y > lowerLimitY)
                        {
                            transform.Translate(Vector3.down * fairySpeed * Time.deltaTime);
                        }
                        else if (this.transform.position.y <= lowerLimitY)
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
                        currentAIState = AI_State_F.CHASING;
                    }
                    else if(playerInAttackRange && playerInRangeX && playerInRangeY)
                    {
                        currentAIState = AI_State_F.ATTACKING;
                    }

                }

                break;

            case AI_State_F.CHASING:

                if (playerInRangeY)
                {
                    if(playerInRangeX)
                    {
                        //Chase until position matches
                        if (!playerInAttackRange)
                        {
                            if (playerManager.playerPosition.y < this.transform.position.y)
                            {
                                transform.Translate(Vector3.down * fairySpeed * Time.deltaTime);
                            }
                            else
                            {
                                transform.Translate(Vector3.up * fairySpeed * Time.deltaTime);
                            }
                        }

                        //Attack
                        else
                        {
                            currentAIState = AI_State_F.ATTACKING;
                        }
                    }
                    else
                    {
                        currentAIState = AI_State_F.PATROLLING;
                    }

                }
                else
                {
                    currentAIState = AI_State_F.PATROLLING;
                }

                break;

            case AI_State_F.ATTACKING:

                //Will attack while seeing the player
                if (canSeePlayer && playerInRangeY)
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
                                }
                                else
                                {
                                    playerManager.lives -= 1;
                                }

                                this.cooldownCounter = cooldownTime;
                            }
                        }
                        //If player moves out of range chase
                        else
                        {
                            currentAIState = AI_State_F.CHASING;
                        }

                    }
                    //If the player dies go back to patrolling
                    else
                    {
                        currentAIState = AI_State_F.PATROLLING;
                        Debug.Log("Patrolling");
                    }
                }
                //Patrol while player is out of line of sight
                else
                {
                    currentAIState = AI_State_F.PATROLLING;
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
