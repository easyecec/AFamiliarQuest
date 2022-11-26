using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinDisplay : MonoBehaviour
{
    PlayerManager playerManager;

    [SerializeField] private Text coinAmount; 

    void Awake()
    {
        playerManager = GameObject.Find("Player").GetComponent<PlayerManager>();
    }

    void Update()
    {
        coinAmount.text = playerManager.Coins.ToString("00");
    }
}
