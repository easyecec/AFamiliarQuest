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
        playerManager.lives = 9;
        playerManager.mageArmor=9;
        Destroy(gameObject);
    }
}
