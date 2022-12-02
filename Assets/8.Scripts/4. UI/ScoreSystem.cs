using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    string scoreKey = "Score";
    string hatKey = "Hats";

    public int CurrentScore { get; set; }
    public int KeyAmount { get; set; }

    private void Awake()
    {
        CurrentScore = PlayerPrefs.GetInt(scoreKey);
        KeyAmount = PlayerPrefs.GetInt(hatKey);
    }

    // Update is called once per frame
    public void SetScore(int score, int hats)
    {
        PlayerPrefs.SetInt(scoreKey, score);
        PlayerPrefs.SetInt(hatKey, hats);

    }
}
