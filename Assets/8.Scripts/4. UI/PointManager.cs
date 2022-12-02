using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PointManager : MonoBehaviour
{
    PlayerManager playerManager;
    CountDownTimer timer;

    [SerializeField] private ScoreStore scoreSO;

    [SerializeField] private int coinPoints;
    [SerializeField] private int lifePoints;
    [SerializeField] private int shieldPoints;
    [SerializeField] private int timeLeft;
    [SerializeField] private int points;
    [SerializeField] private int hats;
    [SerializeField] private int testVariable;

    public int Points
    {
        get { return points; }
    }

    public int Hats
    {
        get { return hats; }
    }

    private void Start()
    {
        playerManager = GameObject.Find("Player").GetComponent<PlayerManager>();
        timer = GameObject.Find("LevelManager").GetComponent<CountDownTimer>();
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
        timeLeft = Mathf.FloorToInt(timer.TimeLeft);

        points = coinPoints + lifePoints + shieldPoints + timeLeft;

        if(points <= 500)
        {
            hats = 0;
        }
        else if(points > 500 && points <= 1000)
        {
            hats = 1;
        }
        else if(points > 1000 && points <= 2000)
        {
            hats = 2;
        }
        else
        {
            hats = 3;
        }

        scoreSO.Hats = hats;
        scoreSO.Score = points;
    }
}
