using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;


public class MovPlayer : MonoBehaviour
{
    [Header("Supervivencia")]
    [SerializeField] int Vida;
    [SerializeField] GameObject Cor1, Cor2 , Cor3,TextoMuerte;
    [SerializeField] bool Daño;
    [SerializeField] float timerdaño,timerReg;
    [SerializeField] bool MouseOff;
    [SerializeField] bool choca;
    bool _sonidoTalar = false;

    [Space]
    [Header("Sonido")]
    [SerializeField] AudioSource sonidoTalar;

    [Space]
    [Header("Camara y Rayo de la camara")]
    [SerializeField] Camera mainCamera; //  Camara Principal
    [SerializeField] Ray ray;           //  rayo camara
    [SerializeField] Vector3 PuntoHit;  //  punto donde se hiso click
    [Space]
    [Header("Variables De movimiento")]
    [SerializeField] float velocidad,velocidadPesado,velocidadLigero,MinDistance;
    float sqrdistanceToMove;
    [SerializeField] Vector3 sentido;
    [SerializeField] float speedturn;
    float sqrMinDistace;
    [SerializeField] bool Move = false;
    [SerializeField] bool CanMove;
    [SerializeField] LayerMask Mask;
    [SerializeField] Collider[] LimitMove;
    [SerializeField] bool PanelActivo;

    [Space]
    [Header("Variables de caida")]
    [SerializeField] float aceleracion;
    [SerializeField] float acelerador;
    [SerializeField] Collider[] Sube, baja, rampa, aire;
    [SerializeField] bool Falling,rampFix;
    [SerializeField] float altura, offset, radio;
    Vector3 SentidoCaida;
    [Space]
    [Header("Animaciones")]
    [SerializeField] Animator animator;
    [SerializeField] float animSpeed;
    

    [Space]
    [Header("Tala")]
    [SerializeField] private bool _talar, talando;
    [SerializeField] GameObject hacha , PanelTala;
    float tiempohacha;
    [SerializeField] LayerMask TreesMask;
    [SerializeField] Vector3 TreePosition;
    Controlador_De_Recursos  _Controlador_De_Recursos;

    [Space]
    [Header("Atacar")]
    [SerializeField] int cantidadLanzas;
    [SerializeField] private bool _atacar, atacando;
    [SerializeField] GameObject lanza , enemigo ,lanzaMov;
    float tiempolanza;
    [SerializeField] LayerMask EnemyMask;
    [SerializeField] Vector3 enemyPos;
    [SerializeField] Vector3 enemySentido;
    [SerializeField] TextMeshProUGUI CantidadLanzas;
    Vector3 lanzaSentido;

    [Space]
    [Header("Picar")]
    [SerializeField] private bool _picar, picando;
    [SerializeField] GameObject Pico, PanelPica;
    float tiempoPico;
    [SerializeField] LayerMask RockMask;
    [SerializeField] Vector3 RockPosition;

    [Space]
    [Header("Items")]
    [SerializeField] GameObject MochilaMadera;
    [SerializeField] GameObject MochilaPiedra;
    [SerializeField] GameObject MochilaMineral;
    [SerializeField] LayerMask Item;
    [SerializeField] GameObject PanelAgarrar;
    [SerializeField] Vector3 ItemPosition;
    [SerializeField] bool AgarrarItemSuelo;
    [SerializeField] float DistanciaItem;
    [SerializeField] GameObject BotonFabricar;
    GameObject ItemToPick;
    [Header("Soltar")]
    [SerializeField] bool soltar;
    [SerializeField] GameObject Madera, Piedra, Mineral;
    

    [Space]
    [Header("Cursor")]
    [SerializeField] Texture2D CursorTalar;
    [SerializeField] Texture2D CursorAtacar;
    [SerializeField] Texture2D CursorAgarrar;
    [SerializeField] Texture2D CursorDefault;
    [SerializeField] Vector2 TalarCursorHotspot, HotspotDefault;

    [Space]
    [Header("Fabricar")]
    [SerializeField] GameObject PanelFabricacion;

    [Space]
    [Header("Construir")]
    [SerializeField] GameObject PanelConstruir;
    [SerializeField] GameObject PanelBP, PanelTable , PanelRomper;
    [SerializeField] LayerMask BP,Table,Construct;
    [SerializeField] BluePrint blueprint;
    [SerializeField] MesaDeTrabajo mesaDeTrabajo;
    [SerializeField] TextMeshProUGUI TxtMadera, TxtPiedra, TxtMineral;
    [SerializeField] GameObject botonMadera,botonPiedra,botonMineral;
    [SerializeField] Color red, orange, green;
    GameObject Romper;


    RaycastHit HitBP;
    RaycastHit HitTree;
    RaycastHit HitRock;
    RaycastHit Hit;
    RaycastHit HitItem;
    RaycastHit HitTable;
    RaycastHit HitConstruct,HitEnemy;


    
    // Start is called before the first frame update
    void Start()
    {
        GameManager.data.Player = this.gameObject;
        MouseOff = false;
        Vida = 3;
        cantidadLanzas = 3;
        CantidadLanzas.text = ("" + cantidadLanzas);
        sqrMinDistace = MinDistance * MinDistance;                             //el cuadrado de la distancia minima para que se mueva
        Falling = false;
        rampFix = true;
        Move = false;
        hacha.SetActive(false);
        CanMove = true;
        PuntoHit = transform.position;
        AgarrarItemSuelo = false;
        soltar = false;
        Daño = false;
        velocidadLigero = velocidad;
        velocidadPesado = velocidad * 0.7f;
        animSpeed = animator.speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Game_Pause.Global_Game_Pause.isPaused)
        {
            animator.speed = animSpeed;
            MovimientoVertical(); // movimiento de caida
            PanelDetector(); // detecta si hay algun panel en pantalla
            Mouse3D();  // Detecto constantemente la posicion del mouse
            MoveTo();  // llamo a las funciones encargadas de mover al jugador      
            RotateTo();  // rota al jugador en hacia su destino
            Talar(); // Accion de Talar
            Atacar(); // Accion de Talar
            Picar(); // Accion Picar    
            AgarrarItem();
            Soltar();
            Construir();
            if (enemigo != null)
            {
                SentidoEnemigo();
            }
            if (Daño)
            {
                DañoEmpuje();
            }
            SpeedSet();
            RegVida();           
        }
        else
        {
            animator.speed = 0;

        }
        
        
    }
    private void Mouse3D()
    {
        if (!MouseOff)
        {
            ray = mainCamera.ScreenPointToRay(Input.mousePosition);              // defino ray como el rayo que sale de la camara
            if (Physics.Raycast(ray, out Hit, 400, Mask) && !PanelActivo && !Physics.Raycast(ray, out HitEnemy, 100, EnemyMask))   // si apretan click mientras que rayo esta tocando 
            {
                if (Input.GetKeyDown(KeyCode.Mouse0) && rampFix == true && CanMove)
                {
                    PuntoHit = Hit.point;
                    Move = true;
                    talando = false;
                    picando = false;
                    AgarrarItemSuelo = false;
                }
                if (!Physics.Raycast(ray, out HitTree, 400, TreesMask) && !Physics.Raycast(ray, out HitRock, 400, RockMask) && !Physics.Raycast(ray, out HitItem, 400, Item) && !Physics.Raycast(ray, out HitTable, 30, Table) && !Physics.Raycast(ray, out HitConstruct, 30, Construct) && !Physics.Raycast(ray, out HitBP, 400, BP) && !Physics.Raycast(ray, out HitEnemy, 100, EnemyMask))
                {
                    Cursor.SetCursor(CursorDefault, HotspotDefault, CursorMode.Auto);  // cursor default (Flecha)
                }


            }
            if (Physics.Raycast(ray, out HitTree, 400, TreesMask) && !PanelActivo && CanMove) // puntero arbol 
            {
                Cursor.SetCursor(CursorTalar, TalarCursorHotspot, CursorMode.Auto);
                if (Input.GetKeyUp(KeyCode.Mouse1))
                {
                    Debug.Log("You Selected a tree");
                    OpenPanel(PanelTala);
                    TreePosition = new Vector3(HitTree.transform.position.x, transform.position.y, HitTree.transform.position.z);
                }
            }
            if (Physics.Raycast(ray, out HitRock, 400, RockMask) && !PanelActivo && CanMove) // puntero piedras
            {
                Cursor.SetCursor(CursorTalar, TalarCursorHotspot, CursorMode.Auto);
                if (Input.GetKeyUp(KeyCode.Mouse1))
                {
                    Debug.Log("You Selected a rock");
                    OpenPanel(PanelPica);
                    RockPosition = new Vector3(HitRock.transform.position.x, transform.position.y, HitRock.transform.position.z);
                }
            }
            if (Physics.Raycast(ray, out HitItem, 400, Item) && !PanelActivo && CanMove && !Physics.Raycast(ray, out HitEnemy, 30, EnemyMask)) // puntero Item
            {
                Cursor.SetCursor(CursorAgarrar, TalarCursorHotspot, CursorMode.Auto);
                if (Input.GetKeyUp(KeyCode.Mouse1))
                {
                    Debug.Log("You Selected a item");
                    OpenPanel(PanelAgarrar);
                    ItemPosition = new Vector3(HitItem.transform.position.x, transform.position.y, HitItem.transform.position.z);
                    ItemToPick = HitItem.collider.gameObject;
                    if (ItemToPick.tag == ("mineral") || ItemToPick.tag == ("lanza"))
                    {
                        BotonFabricar.SetActive(false);
                    }
                    else
                    {
                        BotonFabricar.SetActive(true);
                    }

                }
            }
            if (Physics.Raycast(ray, out HitBP, 400, BP) && !PanelActivo && CanMove)
            {
                Cursor.SetCursor(CursorTalar, TalarCursorHotspot, CursorMode.Auto);
                blueprint = HitBP.collider.gameObject.GetComponent<BluePrint>();
                if (Input.GetKeyDown(KeyCode.Mouse1) && blueprint.Confirmar)
                {
                    OpenPanel(PanelBP);
                }

            }
            if (Physics.Raycast(ray, out HitTable, 30, Table) && CanMove && !Physics.Raycast(ray, out HitItem, 400, Item) && CanMove)
            {
                Cursor.SetCursor(CursorTalar, TalarCursorHotspot, CursorMode.Auto);
                mesaDeTrabajo = HitTable.collider.gameObject.GetComponent<MesaDeTrabajo>();
                ActualizarMateriales();
                OpenPanel(PanelTable);
                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    Romper = HitTable.collider.gameObject;
                    OpenPanel(PanelRomper);
                }
            }
            else
            {
                PanelTable.SetActive(false);
            }
            if (Physics.Raycast(ray, out HitConstruct, 30, Construct) && !PanelActivo && CanMove && !Physics.Raycast(ray, out HitItem, 400, Item) && !PanelActivo && CanMove)
            {
                Cursor.SetCursor(CursorTalar, TalarCursorHotspot, CursorMode.Auto);
                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    Romper = HitConstruct.collider.gameObject;
                    OpenPanel(PanelRomper);
                }
            }
            if (Physics.Raycast(ray, out HitEnemy, 30, EnemyMask) && !PanelActivo && CanMove)
            {
                Cursor.SetCursor(CursorAtacar, TalarCursorHotspot, CursorMode.Auto);
                if (Input.GetKeyUp(KeyCode.Mouse1))
                {
                    enemigo = HitEnemy.collider.gameObject;
                    enemyPos = enemigo.transform.position;
                    _Atacar();
                }

            }
        }
    }
    private void MoveTo()
    {
        LimitMove = Physics.OverlapSphere(transform.position + new Vector3(0,0.2f,0) + sentido.normalized * 0.3f, 0.5f, Mask);  // crea quna esfera en direccion al sentido 0.3 unidades mas adelante y con un radio de 0.5
        foreach (var LimitMove in LimitMove)          //reviso si adentro de LimitMove hay algun objeto
        {
            Move = false; // si hay algun objeto hago que el personaje no se pueda mover
            //Debug.Log(LimitMove.gameObject.name);
        }
        
        PuntoHit.y = transform.position.y;                                     //  anula y actualiza cualquier seleccion en el eje "y"
        sqrdistanceToMove = (PuntoHit - transform.position).sqrMagnitude;      //  distancia entre punto selecionado y jugador al cuadrado para mejorar rendimiento y evitar calculo de distancias con raices
        sentido = PuntoHit - transform.position; //  defino el sentido restando la posicion del jugador al punto seleccionado en la pantalla                              
        if (sqrdistanceToMove > sqrMinDistace && Move)                         //  solo se mueve si la distancia es mayor que la minima para que se mueva Y no hay nada que inpida su movimiento
        {
            animator.SetBool("Caminando", true);
            transform.position = new Vector3(transform.position.x + sentido.normalized.x * Time.deltaTime * velocidad, transform.position.y, transform.position.z + sentido.normalized.z * Time.deltaTime * velocidad);// movimiento sin inportar rotacion
        }
        else // si la distancia es muy chica no se movera mas
        {
            animator.SetBool("Caminando", false);
            Move = false;
        }
    }
    private void RotateTo()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Ataca"))
        {
            Quaternion rotation = Quaternion.LookRotation(enemySentido); // crea una variable rotation y le asigna la rotacion de Sentido
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, speedturn* 1.5f * Time.deltaTime); // transforma la rotacion del jugador suavemente
        }
        if (!Falling && Move && !animator.GetCurrentAnimatorStateInfo(0).IsName("Ataca")) 
        {
            Quaternion rotation = Quaternion.LookRotation(sentido); // crea una variable rotation y le asigna la rotacion de Sentido
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, speedturn * Time.deltaTime); // transforma la rotacion del jugador suavemente
        }
        
    }
    private void MovimientoVertical()
    {

        if (Physics.Raycast(transform.position, Vector3.down, 1f, Mask))// sube
        {
            transform.Translate(0,velocidad*Time.deltaTime* 0.08f ,0);
            Debug.Log("Sube");
            if (Physics.Raycast(transform.position, Vector3.down, 0.9f, Mask))// sube
            {
                transform.Translate(0, velocidad * Time.deltaTime * 0.15f, 0);
                Debug.Log("Sube1");
                if (Physics.Raycast(transform.position, Vector3.down, 0.95f, Mask))// sube
                {
                    transform.Translate(0, velocidad * Time.deltaTime * 0.25f, 0);
                    Debug.Log("Sube2");
                    if (Physics.Raycast(transform.position, Vector3.down, 0.9f, Mask))// sube
                    {
                        transform.Translate(0, velocidad * Time.deltaTime * 0.35f, 0);
                        Debug.Log("Sube3");
                    }
                }
            }
        }
        if (!Physics.Raycast(transform.position + sentido.normalized * 0f, Vector3.down, 1.05f)) 
        {
            
            acelerador = 20f;
            aceleracion += acelerador * Time.deltaTime;
            transform.Translate(0, (aceleracion)* Time.deltaTime * -1, 0);
                       
            if (Physics.Raycast(transform.position, Vector3.down, 1.50f)) // detecta rampa
            {
                Debug.Log("rampa");
                rampFix = true;               
                transform.Translate(0, (velocidad) * Time.deltaTime * -1 *0.05f, 0);
                if (!Physics.Raycast(transform.position, Vector3.down, 1.05f))
                {
                    transform.Translate(0, (velocidad) * Time.deltaTime * -1 * 0.1f, 0);
                    if (!Physics.Raycast(transform.position, Vector3.down, 1.1f))
                    {
                        transform.Translate(0, (velocidad) * Time.deltaTime * -1 * 0.2f, 0);
                        if (!Physics.Raycast(transform.position, Vector3.down, 1.15f))
                        {
                            transform.Translate(0, (velocidad) * Time.deltaTime * -1 * 0.3f, 0);
                            if (!Physics.Raycast(transform.position, Vector3.down, 1.20f))
                            {
                                transform.Translate(0, (velocidad) * Time.deltaTime * -1 * 0.4f, 0);
                            }
                        }
                    }
                }
            }
            else // bool de animacion caida
            {
                Debug.Log("baja");
                rampFix = false;
                animator.SetBool("Caida", true);
            }
            if (!Falling && !rampFix) // direccion mientras cae
            {
                Falling = true;
                Move = false;
                SentidoCaida = sentido * 100;          
            }
            if (Falling && !Move) // caida
            {            
                transform.position = new Vector3(transform.position.x + SentidoCaida.normalized.x * Time.deltaTime * velocidad, transform.position.y, transform.position.z + SentidoCaida.normalized.z * Time.deltaTime * velocidad);
            }
        }
        if (Physics.Raycast(transform.position, Vector3.down, 1.15f))
        {
            aceleracion = 0;
            Falling = false;
            Move = true;
            animator.SetBool("Caida", false);
        }
        

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(Hit.point, 0.3f);
        if (Move)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position + new Vector3(0, 0.2f, 0) + sentido.normalized * 0.3f, 0.5f);
            Gizmos.DrawRay(transform.position - new Vector3(0,transform.localScale.y,0), sentido);
        }
        if (!Move)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position + new Vector3(0, 0.2f, 0) + sentido.normalized * 0.3f, 0.5f);
            Gizmos.DrawRay(transform.position - new Vector3(0, transform.localScale.y, 0), sentido);
        }
        Gizmos.color = Color.red;
        
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(transform.position, Vector3.down * 0.9f);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position + new Vector3(0, 0, 0.01f), Vector3.down * 1.15f);
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position + new Vector3(0, 0, 0.02f), Vector3.down * 1.40f);
        


    }
    public void _Talar() // entrada del boton de talar
    {
        PuntoHit = TreePosition;
        talando = true;
        tiempohacha = 0;
        
    }
    private void Talar() // control de animaciones y talar
    {
        
        if (_talar && !Physics.Raycast(transform.position, sentido.normalized, 4f, TreesMask) && !Move)
        {
            _talar = false;
            talando = false;
            animator.SetBool("talar", false);
        }
        if (talando && Physics.Raycast(transform.position, sentido.normalized, out HitTree, 1.2f, TreesMask))
        {
            CanMove = false;
            animator.SetBool("talar", true);
            _talar = true;
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("talar"))
         {
            
            hacha.gameObject.SetActive(true);
        }
        if (hacha.activeInHierarchy)
        {
            tiempohacha = tiempohacha + Time.deltaTime;
            if (tiempohacha > 2.9f && !_sonidoTalar && Physics.Raycast(transform.position, sentido.normalized, out HitTree, 4f, TreesMask))
            {
                _sonidoTalar = true;

                sonidoTalar.PlayOneShot(sonidoTalar.clip);       
            }
            if (tiempohacha > 5f && Physics.Raycast(transform.position, sentido.normalized, out HitTree, 4f, TreesMask))
            {
                PuntoHit = transform.position;
                _Controlador_De_Recursos = HitTree.collider.gameObject.GetComponent<Controlador_De_Recursos>();
                _Controlador_De_Recursos.Ocultar();
            }        
            if(tiempohacha > 7.4f)
            {   
                hacha.gameObject.SetActive(false);
                CanMove = true;
                _sonidoTalar = false;
                tiempohacha = 0;
            }

     
        }
    }
    public void _Atacar() // entrada del boton de talar
    {
        if (cantidadLanzas > 0)
        {
            atacando = true;
            tiempolanza = 0;
            lanza.gameObject.SetActive(true);
        }
            
    }
    private void Atacar() // control de animaciones y talar
    {
        if (cantidadLanzas > 0)
        {
            if (atacando)
            {
                Debug.Log("aa");
                animator.SetBool("atacar", true);
                _atacar = true;
            }
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Atacar"))
            {
                tiempolanza = 0;
                lanza.gameObject.SetActive(true);
            }
            if (lanza.activeInHierarchy)
            {
                tiempolanza = tiempolanza + Time.deltaTime;
                if (tiempolanza > 0.3)
                {
                    _atacar = false;
                    atacando = false;
                    animator.SetBool("atacar", false);

                }
                if (tiempolanza > 0.62f)
                {
                    cantidadLanzas = cantidadLanzas - 1;
                    CantidadLanzas.text = ("" + cantidadLanzas);
                    Instantiate(lanzaMov, lanza.transform.position, Quaternion.LookRotation(lanzaSentido));
                    lanza.gameObject.SetActive(false);

                }


            }
        }
       
    }
    private void SentidoEnemigo()
    {
        if (enemigo.activeInHierarchy)
        {
            enemySentido = new Vector3(enemyPos.x,transform.position.y,enemyPos.z) - transform.position;
            lanzaSentido = enemyPos - lanza.transform.position;
        }
        
    }
    public void OpenPanel(GameObject panel) // abre un panel
    {
        if(panel == PanelTable && !PanelRomper.activeInHierarchy)
        {
            panel.transform.position = Input.mousePosition + new Vector3(-80,30,0);
        }
        else
        {
            panel.transform.position = Input.mousePosition;
            PanelTable.transform.position = Input.mousePosition + new Vector3(-80, 30, 0);
            Cursor.SetCursor(CursorDefault, HotspotDefault, CursorMode.Auto);
        }
        
        panel.SetActive(true);
    }
    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
    }
    private void PanelDetector() // detecta si hay paneles en pantalla
    {
        if (PanelTala.activeInHierarchy || PanelPica.activeInHierarchy || PanelAgarrar.activeInHierarchy || PanelFabricacion.activeInHierarchy || PanelConstruir.activeInHierarchy || PanelBP.activeInHierarchy || PanelRomper.activeInHierarchy) 
        {
            PanelActivo = true;
        }
        else
        {
            PanelActivo = false;
        }
    }
    public void _Picar()
    {
        PuntoHit = RockPosition;
        picando = true;
        tiempoPico = 0;

    } // control de boton de picar
    private void Picar()
    {
        if (_picar && !Physics.Raycast(transform.position, sentido.normalized, 4f, RockMask) && !Move)
        {
            _picar = false;
            picando = false;
            animator.SetBool("picar", false);
        }
        if (picando && Physics.Raycast(transform.position, sentido.normalized, out HitRock, 1.2f, RockMask))
        {
            CanMove = false;
            animator.SetBool("picar", true);
            _picar = true; // desactiva picar en el proximo frame
            
            
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("picar"))
        {
            
            Pico.gameObject.SetActive(true);
        }
        if (Pico.activeInHierarchy)
        {
            tiempoPico = tiempoPico + Time.deltaTime;
            if (tiempoPico > 4.5f && Physics.Raycast(transform.position, sentido.normalized, out HitRock, 4f, RockMask))
            {
                PuntoHit = transform.position;
                _Controlador_De_Recursos = HitRock.collider.gameObject.GetComponent<Controlador_De_Recursos>();
                _Controlador_De_Recursos.Ocultar();
            }
            if (tiempoPico > 5.8f)
            {
                Pico.gameObject.SetActive(false);
                CanMove = true;
            }


        }
    } // control de animaciones y talar
    public void _Agarrar()
    {
        PuntoHit = ItemPosition;
        AgarrarItemSuelo = true;
    }
    private void AgarrarItem()
    {
        DistanciaItem = (ItemPosition - transform.position).magnitude;
        if (DistanciaItem < 1f && AgarrarItemSuelo)
        {
            if (ItemToPick.tag == ("piedra"))
            {
                Remplazar();
                MochilaPiedra.SetActive(true);
                Destroy(ItemToPick);
                AgarrarItemSuelo = false;
            }
            if (ItemToPick.tag == ("madera"))
            {
                Remplazar();
                MochilaMadera.SetActive(true);
                Destroy(ItemToPick);
                AgarrarItemSuelo = false;
            }
            if (ItemToPick.tag == ("mineral"))
            {
                Remplazar();
                MochilaMineral.SetActive(true);
                Destroy(ItemToPick);
                AgarrarItemSuelo = false;
            }
            if (ItemToPick.tag == ("lanza"))
            {
                cantidadLanzas += 1;
                CantidadLanzas.text = ("" + cantidadLanzas);
                Destroy(ItemToPick);
                AgarrarItemSuelo = false;
            }


        }
    }
    private void Soltar()
    {
        if (Input.GetKeyUp(KeyCode.G))
        {
            Remplazar();
        }
    }
    private void Remplazar()
    {
        if (MochilaMadera.activeInHierarchy)
        {
            if (!Input.GetKey(KeyCode.Space))
            {
                MochilaMadera.SetActive(false);
            }   
            GameObject _Item = Instantiate(Madera, transform.position + new Vector3(0, 1.5f, 0), Quaternion.Euler(-35, 215, 49));
            Rigidbody rb = _Item.GetComponent<Rigidbody>();
            rb.AddForce((sentido.normalized + new Vector3(0,1,0)) * 150);
            soltar = false;
        }
        if (MochilaPiedra.activeInHierarchy)
        {
            if (!Input.GetKey(KeyCode.Space))
            {
                MochilaPiedra.SetActive(false);
            }
            GameObject _Item = Instantiate(Piedra, transform.position + new Vector3(0, 1.5f, 0), Quaternion.Euler(-35, 215, 49));
            Rigidbody rb = _Item.GetComponent<Rigidbody>();
            rb.AddForce((sentido.normalized + new Vector3(0,1,0)) * 150);
            soltar = false;
        }
        if (MochilaMineral.activeInHierarchy)
        {
            if (!Input.GetKey(KeyCode.Space))
            {
                MochilaMineral.SetActive(false);
            }           
            GameObject _Item = Instantiate(Mineral, transform.position + new Vector3(0, 1.5f, 0), Quaternion.Euler(-35, 215, 49));
            Rigidbody rb = _Item.GetComponent<Rigidbody>();
            rb.AddForce((sentido.normalized + new Vector3(0,1,0)) * 150);
            soltar = false;
        }
    }
    private void Construir()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            OpenPanel(PanelConstruir);           
        }
    }
    public void Constructor(GameObject BluePrint) 
    {
        Instantiate(BluePrint, PuntoHit , Quaternion.Euler(0, 180, 0));
    }
    public void _PonerMesa()
    {
        blueprint.Mesa();
    }
    public void BorrarBP()
    {
        GameObject BP = HitBP.collider.gameObject;
        Destroy(BP);
    }
    public void ActualizarMateriales()
    {
        TxtMadera.text = ("Madera:(" + mesaDeTrabajo.mat0 + "/" + mesaDeTrabajo.mat3 + ")");
        TxtPiedra.text = ("Piedra:(" + mesaDeTrabajo.mat1 + "/" + mesaDeTrabajo.mat4 + ")");
        TxtMineral.text = ("Mineral:(" + mesaDeTrabajo.mat2 + "/" + mesaDeTrabajo.mat5 + ")");
        // madera
        if(mesaDeTrabajo.mat0 == 0)
        {
            TxtMadera.color = red;
        }
        else if (mesaDeTrabajo.mat0 < mesaDeTrabajo.mat3 / 2)
        {
            TxtMadera.color = orange;
        }
        else if (mesaDeTrabajo.mat0 < mesaDeTrabajo.mat3)
        {
            TxtMadera.color = Color.yellow;
        }
        else
        {
            TxtMadera.color = green;
        }
        // piedra
        if (mesaDeTrabajo.mat1 == 0)
        {
            TxtPiedra.color = red;
        }
        else if (mesaDeTrabajo.mat1 < mesaDeTrabajo.mat4 / 2)
        {
            TxtPiedra.color = orange;
        }
        else if (mesaDeTrabajo.mat1 < mesaDeTrabajo.mat4)
        {
            TxtPiedra.color = Color.yellow;
        }
        else
        {
            TxtPiedra.color = green;
        }
        // mineral      
        if (mesaDeTrabajo.mat2 == 0)
        {
            TxtMineral.color = red;
        }
        else if (mesaDeTrabajo.mat2 < mesaDeTrabajo.mat5 / 2)
        {
            TxtMineral.color = orange;
        }
        else if (mesaDeTrabajo.mat2 < mesaDeTrabajo.mat5)
        {
            TxtMineral.color = Color.yellow;
        }
        else
        {
            TxtMineral.color = green;
        }
        // ocultar botones
        //madera
        if (mesaDeTrabajo.mat3 == 0)
        {
            botonMadera.SetActive(false);
        }
        else
        {
            botonMadera.SetActive(true);
        }
        //piedra
        if (mesaDeTrabajo.mat4 == 0)
        {
            botonPiedra.SetActive(false);
        }
        else
        {
            botonPiedra.SetActive(true);
        }
        //Mineral
        if (mesaDeTrabajo.mat5 == 0)
        {
            botonMineral.SetActive(false);
        }
        else
        {
            botonMineral.SetActive(true);
        }
    }
    public void _Romper()
    {
        Destroy(Romper);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == ("Enemy"))
        {
            MouseOff = true;
            CanMove = false;
            sentido = (collision.gameObject.transform.position - transform.position);
            Vida = Vida - 1;
            PuntoHit = transform.position;
            Daño = true;
            timerdaño = 0;
            transform.LookAt(new Vector3(collision.transform.position.x, transform.position.y, collision.transform.position.z));
            if (Vida == 3)
            {           
                Cor1.SetActive(true);
                Cor2.SetActive(true);
                Cor3.SetActive(true);
            }
            else if(Vida == 2)
            {
                Cor1.SetActive(true);
                Cor2.SetActive(true);
                Cor3.SetActive(false);
            }
            else if (Vida == 1)
            {
                Cor1.SetActive(true);
                Cor2.SetActive(false);
                Cor3.SetActive(false);
            }
            else if (Vida == 0)
            {
                Cor1.SetActive(false);
                Cor2.SetActive(false);
                Cor3.SetActive(false);
                Game_Pause.Global_Game_Pause.gameObject.GetComponent<BotonStart>().comenzoCargaDeNivel=true;
                TextoMuerte.SetActive(true);
            }
        }
    }
    public void DañoEmpuje()
    {
        timerdaño += Time.deltaTime;      
        if (timerdaño > 0.1f)
        {
            if (!choca)
            {
                transform.Translate(0, 0, -17 * Time.deltaTime);
            }
            
        }
        if (timerdaño > 0.25f)
        {
            PuntoHit = transform.position;
            MouseOff = false;
            CanMove = true;
            timerdaño = 0;
            Daño = false;
        }
        
    }
    private void RegVida()
    {
        if(Vida < 3)
        {
            timerReg += Time.deltaTime;
            if (timerReg > 10)
            {
                Vida = Vida + 1;
                timerReg = 0;
                if (Vida == 3)
                {
                    Cor1.SetActive(true);
                    Cor2.SetActive(true);
                    Cor3.SetActive(true);
                }
                else if (Vida == 2)
                {
                    Cor1.SetActive(true);
                    Cor2.SetActive(true);
                    Cor3.SetActive(false);
                }
            }
        }
    }
    public void Fabricar()
    {
        if(MochilaMadera.activeInHierarchy && ItemToPick.tag == ("piedra"))
        {
            Destroy(ItemToPick);
            MochilaMadera.SetActive(false);
            cantidadLanzas = cantidadLanzas + 3;
            CantidadLanzas.text = ("" + cantidadLanzas);
        }
        if (MochilaPiedra.activeInHierarchy && ItemToPick.tag == ("madera"))
        {
            Destroy(ItemToPick);
            MochilaPiedra.SetActive(false);
            cantidadLanzas = cantidadLanzas + 3;
            CantidadLanzas.text = ("" + cantidadLanzas);
        }
    }
    private void SpeedSet()
    {
        if (MochilaMadera.activeInHierarchy|| MochilaPiedra.activeInHierarchy || MochilaMineral.activeInHierarchy)
        {
            velocidad = velocidadPesado;
        }
        else
        {
            velocidad = velocidadLigero;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
       if(collision.gameObject.tag == ("Obstaculo"))
        {
            transform.Translate(Vector3.up*10*Time.deltaTime);
            choca = true;
        }
    }
    private void OnCollisionExit(Collision coll)
    {
        if (coll.gameObject.tag == ("Obstaculo"))
        {
            choca = false;
        }
    }

}
