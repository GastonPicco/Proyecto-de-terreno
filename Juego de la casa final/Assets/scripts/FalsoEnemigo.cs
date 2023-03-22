using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FalsoEnemigo : MonoBehaviour
{
    public float RangoVision;
    public LayerMask Aproximado;
    bool JugadorCerca;
    public GameObject Jugador;
    public float Velocidad;
    public float FuerzaSalto;
    public float CooldownSalto;
    public bool SaltoSi;
    public LayerMask PisoQue;
    public bool Piso;
    public bool MovRandom = true;
    public float AlturaEnemigo;
    public float Vida = 2;
    private Rigidbody rb;
    public float TiempoCaminar = 10f;
    private Vector3 NewPos;
    private RaycastHit HitPlayerRay;
    private Vector3 SentidoJugador;
    public LayerMask PlayerLayer;
    public float TimerSalto;
    public float Aceleracion;
    public float VelocidadCaida;
    public bool Saltando;
    public bool FramePiso;
    public bool IniciarSalto;
    // Start is called before the first frame update
    void Start()
    {
        Jugador = GameManager.data.Player;
        Debug.Log(Jugador);
        rb = GetComponent<Rigidbody>();
        Saltando = false;
        IniciarSalto = false;
    }

    // Update is called once per frame
    void Update()
    {
        PisoCheck();
        DetectarJugador();
        Mover();
        Salto();
        PisoCheck();

    }
    private void PisoCheck()
    {
        RaycastHit InfoHit = new RaycastHit();

        Debug.DrawRay(transform.position, Vector3.down * 0.7f, Color.green);

        if (Physics.Raycast(transform.position, Vector3.down, out InfoHit, 0.7f, PisoQue))
        {
            Piso = true;
            FramePiso = Piso;

        }
        else
        {
            Piso = false;
            FramePiso = Piso;
        }

    }
    private void OnDrawGizmos()
    {

    }

    public void DetectarJugador()
    {

        SentidoJugador = Jugador.transform.position - transform.position;
        Debug.DrawRay(transform.position, SentidoJugador * 10f, Color.red);

        if (Physics.Raycast(transform.position, SentidoJugador, out HitPlayerRay, 10, PlayerLayer))
        {

            JugadorCerca = true;
        }
        else
        {
            JugadorCerca = false;
        }
    }
    public void Mover()
    {

        if (JugadorCerca)
        {

            Vector3 PosicionJugador = new Vector3(Jugador.transform.position.x, transform.position.y, Jugador.transform.position.z);
            if (!Piso)
            {
                transform.LookAt(PosicionJugador);
                transform.position = Vector3.MoveTowards(transform.position, PosicionJugador, Velocidad * Time.deltaTime);
            }
        }




    }
    void Salto()
    {
        if (Piso)
        {
            TimerSalto += Time.deltaTime;

        }
        if (Input.GetKey(KeyCode.Space))
        {

            transform.position = new Vector3(transform.position.x, (Velocidad * FuerzaSalto - VelocidadCaida) * Time.deltaTime, transform.position.z);


            if (!Piso)
            {
                VelocidadCaida += Aceleracion * Time.deltaTime;
            }
        }


    }

}
