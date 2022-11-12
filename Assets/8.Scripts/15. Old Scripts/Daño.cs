using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Daño : MonoBehaviour
{
    public VidaPlayer vidaPlayer;
    public int daño = 1;
    public float tiempoDeDaño;
    float tiempoDeDañoActual;

    // Start is called before the first frame update
    void Start()
    {
        vidaPlayer = GameObject.FindWithTag("Player").GetComponent<VidaPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider otro)
    {
        if (otro.tag == "Player")
        {
            tiempoDeDañoActual += Time.deltaTime;
            if (tiempoDeDañoActual > tiempoDeDaño)
            {
                vidaPlayer.vidaActual -= daño;
                tiempoDeDañoActual = 0.0f;
            }
        }
    }
}
