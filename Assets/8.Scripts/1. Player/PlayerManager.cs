using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Unity.VisualScripting;

public class PlayerManager : MonoBehaviour
{
    private Animator playerAnim;

    [SerializeField] private int lives;
    [SerializeField] private int coins;
    [SerializeField] private float deadTime;
    [SerializeField] private int tempHitPoints;
    public bool shielded;
    public bool playerDead = false;
    public Vector3 playerPosition;

    public int Lives
    {
        get { return lives; }
        set { lives = value; }
    }

    public int Coins
    {
        get { return coins; }
        set { coins = value; }
    }

    public int TempHitPoints
    {
        get { return tempHitPoints; }
        set { tempHitPoints = value; }
    }


    void Start()
    {
        lives = 9;
        coins = 0;
        shielded = false;
        tempHitPoints= 0;
        deadTime = 2.0f;

        playerAnim = gameObject.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        /* coinAmount.text = coins.ToString();
        livesLeft.text = lives.ToString(); */

        Dead();
        OnDeath(); 
        MageArmor();

        playerPosition = this.transform.position;
    }

    void Dead()
    {
        if (lives <= 0 && !playerDead)
        {
            playerDead = true;
            playerAnim.SetTrigger("Dead");
        }
    }

    void OnDeath()
    {
        if (playerDead)
        {
            if (deadTime > 0)
            {
                this.deadTime -= Time.deltaTime;
            }
            else
            {
                SceneManager.LoadScene("GameOverScreen");
            }
        }
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
        if (other.CompareTag("WorldFalling"))
        {
            lives = 0;
        }

        else if(other.CompareTag("LevelCompleted"))
        {
            SceneManager.LoadScene("LevelCompletedScreen");
        }
    }
}
