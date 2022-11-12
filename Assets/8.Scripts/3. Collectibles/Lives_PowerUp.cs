using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lives_PowerUp : MonoBehaviour
{
    PlayerManager playerManager;

    void Awake()
    {
        playerManager = GameObject.Find("Player").GetComponent<PlayerManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        playerManager.lives += 3;
        Destroy(gameObject);
    }
}
