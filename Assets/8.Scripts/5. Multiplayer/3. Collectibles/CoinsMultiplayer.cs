using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsMultiplayer : MonoBehaviour
{
    PlayerManagerMulty playerManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerManager = other.gameObject.GetComponent<PlayerManagerMulty>();


            playerManager.Coins += 1;
            Destroy(gameObject);
        }
    }
}
