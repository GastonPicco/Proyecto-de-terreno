using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PruebaEnemigo : MonoBehaviour
{
    public Rigidbody rigibody;

    public bool ia;

    public float TimingSalto;
    public float contador;

    public float TimingPersecucion;
    public float contadorPersecucion;

    public bool addForceBool;
    public Vector3 addForceVector;

    public bool setForceBool;
    public Vector3 setForceVector;
    public Vector3 setForceVectorAux;
    public float speed;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ia) {
            if (contador < TimingSalto)
            {
                addForceBool = false;
                contador += 1 * Time.deltaTime;
                setForceVectorAux = Vector3.ClampMagnitude((player.transform.position - this.gameObject.transform.position), 1f);
                
            }
            else
            {
                contador = 0;
                addForceBool = true;
                
                setForceBool = true;

                if (contador < TimingPersecucion)
                {
                    contadorPersecucion += 1 * Time.deltaTime;
                }
                else
                {
                    contadorPersecucion = 0;
                    setForceBool = false;
                }
            }
            
        }

        setForceVector = new Vector3(setForceVectorAux.x, 0, setForceVectorAux.z) * (speed * Time.deltaTime);

        if (addForceBool)
        {
            rigibody.AddForce(addForceVector);
            addForceBool = false;
        }

        if (setForceBool)
        {
            rigibody.AddForce(setForceVector, ForceMode.Acceleration);
        }
    }
}
