using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsCollectible : MonoBehaviour
{
    PlayerManager playerManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerManager = other.gameObject.GetComponent<PlayerManager>();


            playerManager.Coins += 1;
            Destroy(gameObject);
        }
    }
}
