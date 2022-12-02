using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManagerMulty : MonoBehaviour
{
    private Animator playerAnim;

    [SerializeField] private int coins;
    [SerializeField] private bool gameOver;
    [SerializeField] private bool playerDead;

    public bool PlayerDead
    {
        get { return playerDead; }
        set { playerDead = value; }
    }


    public Vector3 playerPosition;

    public int Coins
    {
        get { return coins; }
        set { coins = value; }
    }

    void Start()
    {
        coins = 0;
        playerDead = false;
        playerAnim = gameObject.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        /* coinAmount.text = coins.ToString();
        livesLeft.text = lives.ToString(); */

        playerPosition = this.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WorldFalling"))
        {
            playerDead = true;
        }
    }
}
