using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinAmount;
    [SerializeField] private TextMeshProUGUI livesLeft;
    public int lives;
    public int coins;
    public int tempHitPoints;
    public bool shielded;
    public bool playerDead = false;
    public Vector3 playerPosition;

    void Start()
    {
        lives = 9;
        coins = 0;
        shielded = false;
        tempHitPoints= 0;
    }

    // Update is called once per frame
    void Update()
    {
        /* coinAmount.text = coins.ToString();
        livesLeft.text = lives.ToString(); */

        Lives();
        Dead();

    }

    void Lives()
    {
        if(lives <= 0)
        {
            playerDead = true;
            Debug.Log("The player is dead");
        }
    }

    void Dead()
    {

    }

    void MageArmor()
    {
        if (tempHitPoints > 0)
        {
            shielded= true;
        }
        else
        {
            shielded= false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MapEnd")
        {
            //PlayerDead is usded also to trigger the dead animation and lose screen... we should have a win screen 
            //playerDead=true;

            SceneManager.LoadScene("GameOverScreen");
        }

        else if(other.tag == "LevelCompleted")
        {
            SceneManager.LoadScene("LevelCompletedScreen");
        }
    }
}
