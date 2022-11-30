using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsDisplay : MonoBehaviour
{
    //Hats - Amount

    private int hatNumber = 3;

    [SerializeField] private Image[] hats;
    [SerializeField] private Sprite fullHat;
    [SerializeField] private Sprite emptyHat;

    [SerializeField] PointManager pointManager;

    //Score - Number
    [SerializeField] private Text pointsAmount;

    void Awake()
    {
        pointManager = GameObject.Find("PointManager").GetComponent<PointManager>();
    }

    void Update()
    {
        pointsAmount.text = pointManager.Points.ToString();

        hatNumber = pointManager.Hats;

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
