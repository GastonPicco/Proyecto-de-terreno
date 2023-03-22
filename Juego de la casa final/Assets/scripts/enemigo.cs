using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemigo : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject jugador;
    Rigidbody rb;
    Vector3 ActualVelocidadrb, velocityrb;
    [SerializeField] float fuerza,largoRayCast, radioWireSphere, largoRayCastHead, largoRayCastObstaculo,timerSalto, velocidad,DistanciaPuntoIteres,multiplicador;
    [SerializeField] LayerMask Obstaculos, playerLayer;
    [SerializeField] bool piso, jugadorCerca;
    [SerializeField] Collider[] tocaPiso;
    [SerializeField] bool tocaSuelo;
    [SerializeField] Vector3 zonaEnemgio,puntoInteres, sentidoInteres, sentidoPlayerFixed, sentidoInteresFixed;
    [SerializeField] float Rnd;
    [SerializeField] AudioSource rebote;
    [SerializeField] GameObject cuerpo,eye;
    [SerializeField] private Vector3 startPos, targetPos;
    [SerializeField] private float lerpDuration;
    [SerializeField] AnimationCurve curva;
    public bool wasPaused;
    public bool setVelocity;
    Vector3 sentidoJugador;
    void Start()
    {
        jugador = GameManager.data.Player;
        Debug.Log(jugador);
        tocaSuelo = true;
        zonaEnemgio = transform.position;
        puntoInteres = new Vector3(zonaEnemgio.x + Random.Range(-10, 10f), transform.position.y , (zonaEnemgio.z + Random.Range(-10, 10f)));
        rb = gameObject.GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (Game_Pause.Global_Game_Pause.isPaused)
        {
            wasPaused = true;
            if(setVelocity == false)
            {
                velocityrb = rb.velocity;
                setVelocity = true;
                rb.isKinematic = true;
            }
          
        }
        else
        {
            
            if (wasPaused)
            {
                setVelocity = false;
                rb.isKinematic = false;
                rb.velocity = velocityrb;
                wasPaused = false;               
            }
            Piso();
            RangoAtaque();
            Salto();
            Mover();
        }
        /*if(rb.isKinematic == false)
        {
            Piso();
            RangoAtaque();
            Salto();
            Mover();
        }*/
            
    }
    private void Piso()
    {
        tocaPiso = Physics.OverlapSphere(transform.position + new Vector3(0, largoRayCast, 0), radioWireSphere, Obstaculos);
        if (tocaPiso.Length==0)
        {
            piso = false;
            
        }
        else
        {
            timerSalto += Time.deltaTime;
            piso = true;
        }
        tocaPiso = Physics.OverlapSphere(transform.position + new Vector3(0, largoRayCastHead, 0), radioWireSphere/2, Obstaculos);
    }
    private void RangoAtaque()
    {
        sentidoJugador = jugador.transform.position - transform.position;
        sentidoPlayerFixed = new Vector3(jugador.transform.position.x, transform.position.y, jugador.transform.position.z) - transform.position;
        sentidoInteres = puntoInteres - transform.position;
        sentidoInteresFixed = new Vector3(puntoInteres.x, transform.position.y, puntoInteres.z) - transform.position;
        DistanciaPuntoIteres = (transform.position - puntoInteres).magnitude;
        RaycastHit HitPlayerRay = new RaycastHit();
        if (Physics.Raycast(transform.position, sentidoJugador, out HitPlayerRay, 10, playerLayer))
        {
            jugadorCerca = true;
            Debug.DrawRay(transform.position, sentidoJugador * 10f, Color.red);
            sentidoJugador = jugador.transform.position - transform.position;
        }
        else
        {
            jugadorCerca = false;
            Debug.DrawRay(transform.position, sentidoInteres.normalized * DistanciaPuntoIteres, Color.red);
        }
    }
    private void Salto()
    {
        RaycastHit InfoHit = new RaycastHit();
        Debug.DrawRay(transform.position + new Vector3(0, 0.7f, 0), sentidoInteres.normalized * largoRayCastObstaculo, Color.blue);
        if (Physics.Raycast(transform.position + new Vector3(0, 0.7f, 0), sentidoInteres.normalized, out InfoHit, largoRayCastObstaculo, Obstaculos))
        {
            multiplicador = 2;
        }
        if (!Physics.Raycast(transform.position + new Vector3(0, 0.7f, 0), sentidoInteres.normalized, out InfoHit, largoRayCastObstaculo, Obstaculos))
        {
            multiplicador = 1;
        }
        if (tocaSuelo &&piso && timerSalto > Rnd)
        {
            tocaSuelo = false;
            Rigidbody RB = gameObject.GetComponent<Rigidbody>();
            RB.AddForce(0, fuerza * 100 * multiplicador, 0);
            tocaSuelo = false;
        }
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        Rnd = Random.Range(0.5f, 3f);
        timerSalto = 0;
        tocaSuelo = true;
        {
            rebote.PlayOneShot(rebote.clip);
            if (DistanciaPuntoIteres < 2f)
            {
                
                puntoInteres = new Vector3(zonaEnemgio.x + Random.Range(-10, 10f), transform.position.y, (zonaEnemgio.z + Random.Range(-10, 10f)));
            }
                
        }
        if (collision.gameObject.tag == "Obstaculo" && piso)
        {
            StartCoroutine(LerpScale(targetPos, startPos, lerpDuration/2));
        }
    }
    private void OnCollisionExit(Collision coll)
    {
        if(coll.gameObject.tag=="Obstaculo"&& piso)
        {
            StartCoroutine(LerpScale(startPos, targetPos, lerpDuration));
        }
    }
    public void Mover()
    {
        puntoInteres.y = transform.position.y;
        if (jugadorCerca)
        {
            if (piso)
            {
                Quaternion rotation = Quaternion.LookRotation(sentidoPlayerFixed); //
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, velocidad * 1.5f * Time.deltaTime); // 
            }            
            Vector3 PosicionJugador = new Vector3(jugador.transform.position.x, transform.position.y, jugador.transform.position.z);
            if (!piso)
            {
                //transform.position = (new Vector3(transform.position.x + sentidoPlayerFixed.normalized.x * velocidad * Time.deltaTime, transform.position.y, transform.position.z + sentidoPlayerFixed.normalized.z * velocidad * Time.deltaTime));
                transform.Translate(Vector3.forward * velocidad * Time.deltaTime);
            }
        }
        if (!jugadorCerca)
        {
            if (!piso)
            {
                Quaternion rotation = Quaternion.LookRotation(sentidoInteresFixed); //
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, velocidad * 7.5f * Time.deltaTime); // 
                transform.position = (new Vector3(transform.position.x + sentidoInteres.normalized.x * velocidad * Time.deltaTime, transform.position.y, transform.position.z + sentidoInteres.normalized.z * velocidad * Time.deltaTime));
            }           
        }
    }
    IEnumerator LerpScale(Vector3 startPos, Vector3 targetPos, float lerpDuration)
    {
        float timeElapsed = 0f;
        while(timeElapsed<lerpDuration)
        {   
            cuerpo.transform.localScale = Vector3.Lerp(startPos, targetPos, curva.Evaluate(timeElapsed / lerpDuration));
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        cuerpo.transform.localScale = targetPos;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position + new Vector3(0, largoRayCast, 0),radioWireSphere);
    }

}
