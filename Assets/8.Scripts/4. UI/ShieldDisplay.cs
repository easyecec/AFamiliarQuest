using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldDisplay : MonoBehaviour
{
    //Variables

    private int shield;

    [SerializeField] private Image[] shields;

    PlayerManager playerManager;

    void Awake()
    {
        playerManager = GameObject.Find("Player").GetComponent<PlayerManager>();
    }

    void Update()
    {
        shield = playerManager.TempHitPoints;

        for (int i = 0; i < shields.Length; i++)
        {
            if (i < shield)
            {
                shields[i].enabled = true;
            }

            else
            {
                shields[i].enabled = false;
            }
        }
    }
}
