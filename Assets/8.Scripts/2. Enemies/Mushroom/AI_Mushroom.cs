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

    //Stores the animators
    [SerializeField] private Animator catAnim;
    [SerializeField] private Animator mushroomAnim;

    [SerializeField] private bool canSeePlayer;

    private float cooldownCounter; //Tracks the cooldown time

    void Awake()
    {
        playerManager = GameObject.Find("Player").GetComponent<PlayerManager>();
        mushroomAnim = gameObject.GetComponentInChildren<Animator>();
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

                        if(playerManager.shielded)
                        {

                            //StartCoroutine(DamageDelay());
                            //playerManager.tempHitPoints -= 1;
                            mushroomAnim.SetTrigger("Attacking");
                        }
                        else
                        {
                            StartCoroutine(DamageDelay());
                            mushroomAnim.SetTrigger("Attacking");
                        }
                            
                        this.cooldownCounter = cooldownTime;

                        currentAIState = AI_State_M.CHARGING;
                    }

                    else
                    {
                        this.cooldownCounter = cooldownTime;
                        currentAIState = AI_State_M.IDLE;
                    }
                }

                else
                {
                    currentAIState = AI_State_M.IDLE;
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
                mushroomAnim.SetBool("Charging", true);
            }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            {
                canSeePlayer = false;
                mushroomAnim.SetBool("Charging", false);
            }
    }

    IEnumerator DamageDelay()
    {

        //Wait for 2 seconds
        yield return new WaitForSeconds(1);
        catAnim.SetTrigger("Damaged");
        playerManager.Lives -= 1;

    }

}
