using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Enemy states
public enum AI_State
{
    PATROLLING, CHASING, ATTACKING
}

public class AI_Fairy : MonoBehaviour
{
    PlayerManager playerManager;

    //AI_States variables

    [SerializeField] private AI_State currentAIState; //Saves the current enum value (current state)

    [SerializeField] private float cooldownTime = 1f; //Time between each attack

    private bool canSeePlayer;

    private float cooldownCounter; //Tracks the cooldown time

    [SerializeField] private float patrolRange; //Limits area of movement

    [SerializeField] private Vector3 fairyPosition; //Tracks position, remember to use this.GameObject for consistency

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


    //method that tracks the AI_States
    void enemyStates()
    {
        //Create Switch to determine AI state
        switch (currentAIState)
        {
            case AI_State.PATROLLING:

                if (!canSeePlayer)
                {
                    //Will go up until patrol limit
                    if (movePositive)
                    {
                        if (this.GameObject.fairyPosition.y < patrolRange)
                        {
                            this.GameObject.fairyPosition.y += 0.1 * Time.deltaTime;
                        }
                        else
                        {
                            movePositive = false;
                        }
                    }
                    else
                    {
                        //Will go down until patrol limit
                        if (this.GameObject.fairyPosition.y > -patrolRange)
                        {
                            this.GameObject.fairyPosition.y -= 0.1 * Time.deltaTime;
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
                    if (this.GameObject.fairyPosition.y != playerManager.playerPosition.y)
                    {
                        currentAIState = AI_State.CHASING;
                    }
                    else
                    {
                        currentAIState = AI_State.ATTACKING;
                    }
                }

                break;

            case AI_State.CHASING:

                if (canSeePlayer)
                {
                    //Chase until position matches
                    if (this.GameObject.fairyPosition.y != playerManager.playerPosition.y)
                    {
                        //Chase player up
                        if (this.GameObject.fairyPosition.y < playerManager.playerPosition.y)
                        {
                            this.GameObject.fairyPosition.y += 0.1 * Time.deltaTime;
                        }
                        //Chase player down
                        if (this.GameObject.fairyPosition.y > playerManager.playerPosition.y)
                        {
                            this.GameObject.fairyPosition.y -= 0.1 * Time.deltaTime;
                        }
                    }

                    //Attack
                    else
                    {
                        currentAIState = AI_State.ATTACKING;
                    }

                }
                else
                {
                    currentAIState = AI_State.PATROLLING;
                }

                break;

            case AI_State.ATTACKING:
                //Will attack while seeing the player
                if (canSeePlayer)
                {
                    //Attack player while the player is alive
                    if (!playerManager.playerDead)
                    {
                        //Keep attacking while in same position as player
                        if (this.GameObject.fairyPosition.y == playerManager.playerPosition.y)
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
                            currentAIState = AI_State.CHASING;
                        }
                    }
                    //If the player dies go back to patrolling
                    else
                    {
                        currentAIState = AI_State.PATROLLING;
                        Debug.Log("Patrolling");
                    }
                }
                //Patrol while player is out of line of sight
                else
                {
                    currentAIState = AI_State.PATROLLING;
                    Debug.Log("Patrolling");
                }

                break;
        }
    }

    private void OnTriggerEnter2D(Collider other)
    {
        if (other.tag == "Player")
        {
            canSeePlayer = true;
            Debug.Log("PlayerEnteredTheArea");
        }
    }

    private void OnTriggerExit2D(Collider other)
    {
        if (other.tag == "Player")
        {
            canSeePlayer = false;
            Debug.Log("PlayerExitedTheArea");
        }
    }
}
