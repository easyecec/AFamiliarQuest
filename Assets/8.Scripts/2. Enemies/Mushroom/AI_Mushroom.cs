using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// Enemy states
public enum AI_State_M
{
    IDLE, CHARGING, ATTACKING 
}


public class AI_Mushroom : MonoBehaviour
{
    PlayerManager playerManager;

    //AI_States variables

    [SerializeField] private AI_State_M currentAIState; //Saves the current enum value (current state)

    [SerializeField] private float cooldownTime = 2f; //Time between each attack

    private bool canSeePlayer;

    private float cooldownCounter; //Tracks the cooldown time

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
            case AI_State_M.IDLE:

                if (canSeePlayer == true)
                {
                    currentAIState = AI_State_M.CHARGING;
                    cooldownCounter = cooldownTime;
                    Debug.Log("Charging");
                }

                break;

            case AI_State_M.CHARGING:

                if (cooldownCounter > 0)
                {
                    Debug.Log("Charging");
                    Debug.Log(cooldownCounter);
                    cooldownCounter -= Time.deltaTime;
                }

                else 
                {
                    currentAIState = AI_State_M.ATTACKING;
                }

                break;

            case AI_State_M.ATTACKING:

                if (canSeePlayer == true)
                {
                    if (playerManager.playerDead == false)
                    {
                        //Attacks player
                        Debug.Log("Attacking");

                        if(playerManager.shielded)
                        {
                            if (playerManager.mageArmor == playerManager.lives)
                            {
                                playerManager.mageArmor -= 1;
                            }
                            else
                            {
                                playerManager.lives-= 1;
                            }
                            
                            cooldownCounter = cooldownTime;

                            currentAIState = AI_State_M.CHARGING;
                        }
                        else
                        {
                            playerManager.lives -= 1;

                            cooldownCounter = cooldownTime;

                            currentAIState = AI_State_M.CHARGING;
                        }
                       
                    }

                    else
                    {
                        currentAIState = AI_State_M.IDLE;
                        Debug.Log("Idle");
                    }
                }

                else
                {
                    currentAIState = AI_State_M.IDLE;
                    Debug.Log("Idle");
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
