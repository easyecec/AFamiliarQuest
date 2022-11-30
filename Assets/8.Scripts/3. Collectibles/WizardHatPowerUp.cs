using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardHatPowerUp : MonoBehaviour
{
    PlayerManager playerManager;

    private void OnTriggerEnter(Collider other)
    {
        playerManager = other.gameObject.GetComponent<PlayerManager>();

        playerManager.Lives = 9;
        playerManager.TempHitPoints = 5;
        Destroy(gameObject);
    }
}
