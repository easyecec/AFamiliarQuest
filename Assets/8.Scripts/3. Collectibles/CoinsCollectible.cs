using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsCollectible : MonoBehaviour
{
    PlayerManager playerManager;

    void Awake()
    {
        playerManager = GameObject.Find("Player").GetComponent<PlayerManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        playerManager.coins += 1;
        Destroy(gameObject);
    }
}
