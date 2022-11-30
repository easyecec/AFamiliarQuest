using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointManager : MonoBehaviour
{
    PlayerManager playerManager;

    private int coinPoints;
    private int lifePoints;
    private int shieldPoints;
    private int points;
    private int hats;

    public int Points
    {
        get { return points; }
    }

    public int Hats
    {
        get { return hats; }
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        playerManager = GameObject.Find("Player").GetComponent<PlayerManager>();

    }

    private void Update()
    {
        PointCount();
    }

    private void PointCount()
    {
        coinPoints = playerManager.Coins * 50;
        lifePoints = playerManager.Lives * 50;
        shieldPoints = playerManager.TempHitPoints * 100;

        points = coinPoints + lifePoints + shieldPoints;

        if(points <= 375)
        {
            hats = 0;
        }
        else if(points > 375 && points <= 750)
        {
            hats = 1;
        }
        else if(points > 750 && points <= 1125)
        {
            hats = 2;
        }
        else
        {
            hats = 3;
        }
    }
}
