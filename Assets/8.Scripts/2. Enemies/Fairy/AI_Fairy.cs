using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Enemy states
public enum AI_State_F
{
    PATROLLING, CHASING, ATTACKING
}

public class AI_Fairy : MonoBehaviour
{
    PlayerManager playerManager;

    //AI_State variables

    [SerializeField] private AI_State_F currentAIState; //Saves the current enum value (current state)

    [SerializeField] private float cooldownTime = 1f; //Time between each attack

    private bool canSeePlayer;

    private float cooldownCounter; //Tracks the cooldown time

    [SerializeField] private float patrolRange; //Limits area of movement

    [SerializeField] private Vector2 fairyPosition; //Tracks position, remember to use this.GameObject for consistency

    private bool movePositive; // Decides whether the fairy moves up or down


    void Awake()
    {
        playerManager = GameObject.Find("Player").GetComponent<PlayerManager>();
        cooldownCounter = cooldownTime;
    }

    void Update()
    {
        enemyStates();
    }


    //method that tracks the AI_State
    void enemyStates()
    {
        //Create Switch to determine AI state
        switch (currentAIState)
        {
            case AI_State_F.PATROLLING:

                if (!canSeePlayer)
                {
                    //Will go up until patrol limit
                    if (movePositive)
                    {
                        if (this.fairyPosition.y < patrolRange)
                        {
                            this.fairyPosition.y += 0.1f * Time.deltaTime;
                        }
                        else
                        {
                            movePositive = false;
                        }
                    }
                    else
                    {
                        //Will go down until patrol limit
                        if (this.fairyPosition.y > patrolRange)
                        {
                            this.fairyPosition.y -= 0.1f * Time.deltaTime;
                        }
                        else
                        {
                            movePositive = true;
                        }
                    }
                    Debug.Log("Patrolling");
                }
                else
                {
                    if (this.fairyPosition.y != playerManager.playerPosition.y)
                    {
                        currentAIState = AI_State_F.CHASING;
                    }
                    else
                    {
                        currentAIState = AI_State_F.ATTACKING;
                    }
                }

                break;

            case AI_State_F.CHASING:

                if (canSeePlayer)
                {
                    //Chase until position matches
                    if (this.fairyPosition.y != playerManager.playerPosition.y)
                    {
                        //Chase player up
                        if (this.fairyPosition.y < playerManager.playerPosition.y)
                        {
                            this.fairyPosition.y += 0.1f * Time.deltaTime;
                        }
                        //Chase player down
                        if (this.fairyPosition.y > playerManager.playerPosition.y)
                        {
                            this.fairyPosition.y -= 0.1f * Time.deltaTime;
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

                break;

            case AI_State_F.ATTACKING:
                //Will attack while seeing the player
                if (canSeePlayer)
                {
                    //Attack player while the player is alive
                    if (!playerManager.playerDead)
                    {
                        //Keep attacking while in same position as player
                        if (this.fairyPosition.y == playerManager.playerPosition.y)
                        {
                            //Make sure cooldown is on before attacking
                            if (cooldownCounter == 0)
                            {
                                //Attacks player
                                Debug.Log("Attacking");

                                //Damage changes based on power-up effect
                                if (playerManager.shielded)
                                {
                                    if (playerManager.mageArmor == playerManager.lives)
                                    {
                                        playerManager.mageArmor -= 1;
                                    }
                                    else
                                    {
                                        playerManager.lives -= 1;
                                    }

                                    cooldownCounter = cooldownTime;
                                }
                                //Damage if no power-up is in effect
                                else
                                {
                                    playerManager.lives -= 1;
                                    cooldownCounter = cooldownTime;

                                }
                            }
                            //If cooldown is not reset cooldown before attacking
                            else
                            {
                                cooldownCounter -= Time.deltaTime;
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canSeePlayer = true;
            Debug.Log("PlayerEnteredTheArea");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canSeePlayer = false;
            Debug.Log("PlayerExitedTheArea");
        }
    }
}
