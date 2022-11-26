using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardHatPowerUp : MonoBehaviour
{
    PlayerManager playerManager;

    void Awake()
    {
        playerManager = GameObject.Find("Player").GetComponent<PlayerManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        playerManager.Lives = 9;
        playerManager.tempHitPoints=9;
        Destroy(gameObject);
    }
}
