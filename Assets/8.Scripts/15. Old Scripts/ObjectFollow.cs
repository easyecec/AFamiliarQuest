using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Custum/Camara/Camara 2D")]
public class ObjectFollow : MonoBehaviour
{ 
    public Transform objectivoCamara;
    public float distancia = 0.0f;
    public float velocidadDesplazamiento = 12.0f;

    private float posicionCamaraX = 0.0f;
    private float posicionPersonajeX = 0.0f;
    private float posicionCamaraY = 0.0f;
    private float posicionPersonajeY = 0.0f;
    private Transform refTransform;

    // Start is called before the first frame update
    void Start()
    {
        refTransform = this.GetComponent<Transform>();

        refTransform.position = new Vector3(objectivoCamara.position.x, objectivoCamara.position.y, distancia);
        posicionCamaraX = objectivoCamara.position.x;
        posicionCamaraY = objectivoCamara.position.y;

    }

    // Update is called once per frame
    void Update()
    {

    }
    
    private void LateUpdate()
    {
        posicionPersonajeX = objectivoCamara.position.x;
        posicionPersonajeY = objectivoCamara.position.y;

        posicionCamaraX = Mathf.Lerp(posicionCamaraX, posicionPersonajeX, velocidadDesplazamiento * Time.deltaTime);
        posicionCamaraY = Mathf.Lerp(posicionCamaraY, posicionPersonajeY, velocidadDesplazamiento * Time.deltaTime);

        refTransform.position = new Vector3(posicionCamaraX, posicionCamaraY, distancia);
    }
}
