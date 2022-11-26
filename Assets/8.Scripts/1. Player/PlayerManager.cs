using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class PlayerManager : MonoBehaviour
{
    private Animator playerAnim;
    [SerializeField] private TextMeshProUGUI coinAmount;
    [SerializeField] private TextMeshProUGUI livesLeft;
    [SerializeField] private int lives;
    [SerializeField] private int coins;

    public int tempHitPoints;
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


    void Start()
    {
        lives = 9;
        coins = 0;
        shielded = false;
        tempHitPoints= 0;

        playerAnim = gameObject.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        /* coinAmount.text = coins.ToString();
        livesLeft.text = lives.ToString(); */

        Dead();
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
        if (other.tag == "WorldFalling")
        {
            lives = 0;
        }

        else if(other.tag == "LevelCompleted")
        {
            SceneManager.LoadScene("LevelCompletedScreen");
        }
    }
}
