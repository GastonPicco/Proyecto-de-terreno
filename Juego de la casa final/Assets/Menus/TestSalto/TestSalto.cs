using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSalto : MonoBehaviour
{
    public float tiempoParaSalto;
    public float contadorParaSalto;
    public float tiempoEnElAire;
    public float contadorEnElAire;

    public float margenPiso;
    public float velocidadSalto;

    public bool isGrounded;
    public LayerMask layerPiso;

    private RaycastHit HitPlayerRay;
    public RaycastHit InfoHit;
    public string InfoHitName;

    public bool iniciarSalto;
    public bool iniciarCaida;

    private void PisoCheck()
    {

        Debug.DrawRay(transform.position, Vector3.down * margenPiso, Color.green);

        if (Physics.Raycast(transform.position, Vector3.down, out InfoHit, margenPiso, layerPiso))
        {
            InfoHitName = InfoHit.collider.gameObject.name;
            isGrounded = true;

        }
        else
        {
            InfoHitName = "nada";
            isGrounded = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PisoCheck();

        if (isGrounded)
        {
            iniciarCaida = false;

            if (contadorParaSalto < tiempoParaSalto)
            {
                
                contadorParaSalto += 1 * Time.deltaTime;
            }
            else
            {
                if (!iniciarSalto)
                {
                    contadorParaSalto = 0;
                    iniciarCaida = false;
                    iniciarSalto = true;
                    IniciarSalto();
                }
                
            }

        }
        else
        {
            contadorParaSalto = 0;

            if (contadorEnElAire < tiempoEnElAire)
            {
                IniciarSalto();
                contadorEnElAire += 1 * Time.deltaTime;
            }
            else
            {
                if (!iniciarCaida)
                {
                    contadorEnElAire = 0;
                    iniciarSalto = false;
                    iniciarCaida = true;
                    IniciarCaida();
                }

                IniciarCaida();
            }
        }
    }

    public void IniciarCaida() 
    {
        transform.Translate( Vector3.down * (velocidadSalto * Time.deltaTime));
    }
    public void IniciarSalto() 
    {
        transform.Translate(Vector3.up * (velocidadSalto * Time.deltaTime));
    }
}
