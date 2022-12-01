using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownTimer : MonoBehaviour
{
    private float timer;

    [SerializeField] private Text counterDisplay;
    [SerializeField] private float timeDuration = 300f;

    PlayerManager playerManager;

    void Start()
    {
        ResetTimer();

        playerManager = GameObject.Find("Player").GetComponent<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
            UpdateTimerDisplay(timer);
        }
        else
        {
            TimeOut();
        }
    }

    private void ResetTimer()
    {
        timer = timeDuration;
    }

    private void UpdateTimerDisplay(float time)
    {
        float seconds = Mathf.FloorToInt(time);

        counterDisplay.text = seconds.ToString("000");
    }

    private void TimeOut()
    {
        if(timer != 0)
        {
            timer = 000;
            UpdateTimerDisplay(timer);

            playerManager.Lives = 0;
        }
    }
}
