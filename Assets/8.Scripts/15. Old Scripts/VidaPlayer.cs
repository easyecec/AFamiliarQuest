using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class VidaPlayer : MonoBehaviour
{
    [SerializeField] private TMP_Text uiText;
    public int vidaMaxima = 9;
    public int vidaActual;

    private void Start()
    {
        vidaActual = vidaMaxima;
    }

    // Update is called once per frame
    void Update()
    {
        uiText.text= "<sprite=0>            x " + vidaActual.ToString();
    }
}
