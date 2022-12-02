using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsDisplay : MonoBehaviour
{

    [SerializeField] private ScoreStore scoreSO;

    //Hats - Amount

    private int hatNumber = 3;

    [SerializeField] private Image[] hats;
    [SerializeField] private Sprite fullHat;
    [SerializeField] private Sprite emptyHat;


    //Score - Number
    [SerializeField] private Text pointsAmount;

    void Update()
    {
        pointsAmount.text = scoreSO.Score.ToString();

        hatNumber = scoreSO.Hats;

        for (int i = 0; i < hats.Length; i++)
        {
            if (i < hatNumber)
            {
                hats[i].sprite = fullHat;
            }

            else
            {
                hats[i].sprite = emptyHat;
            }
        }
    }
}
