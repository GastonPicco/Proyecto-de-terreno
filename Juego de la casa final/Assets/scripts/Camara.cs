using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour
{
    [SerializeField] GameObject Jugador;
    [SerializeField] float posicion;
    [SerializeField] float Offsety, OffsetZ;
    [SerializeField] bool Cam;



    private void Awake()
    {

    }

    void Update()
    {
        
        if (Cam)
        {
            transform.localPosition = new Vector3(0, Offsety + posicion, OffsetZ + (posicion));
            if(posicion >= -5)
            {
                if (Input.GetAxis("Mouse ScrollWheel") > 0)
                {
                    posicion = posicion - 0.5f;
                }
            }
            if (posicion <= 7)
            {
                if (Input.GetAxis("Mouse ScrollWheel") < 0)
                {
                    posicion = posicion + 0.5f;
                }
            }

        }
        else
        {
            transform.localPosition = Jugador.transform.position + new Vector3(0, Offsety + posicion, OffsetZ + (posicion)); 
        }
        
    }
}
