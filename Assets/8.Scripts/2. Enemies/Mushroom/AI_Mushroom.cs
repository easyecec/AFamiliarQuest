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

    [SerializeField] private Animator catAnim;

    [SerializeField] private bool canSeePlayer;

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
                    this.cooldownCounter -= Time.deltaTime;
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
                            playerManager.tempHitPoints -= 1;
                            catAnim.SetTrigger("Damaged");
                        }
                        else
                        {
                            playerManager.lives-= 1;
                            catAnim.SetTrigger("Damaged");
                        }
                            
                        this.cooldownCounter = cooldownTime;

                        currentAIState = AI_State_M.CHARGING;
                    }

                    else
                    {
                        this.cooldownCounter = cooldownTime;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            {
                canSeePlayer = true;
                catAnim = other.gameObject.GetComponent<Animator>();
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
