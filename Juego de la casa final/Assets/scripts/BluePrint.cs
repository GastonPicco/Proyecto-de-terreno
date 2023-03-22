using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePrint : MonoBehaviour
{
    [Header("Camara y Rayo de la camara")]
    [SerializeField] Camera mainCamera; //  Camara Principal
    [SerializeField] Ray ray;           //  rayo camara
    [SerializeField] Vector3 PuntoHit;  //  punto donde se hiso click
    [SerializeField] Color rojo,azul;
    RaycastHit BPHit;
    RaycastHit Hit;
    [SerializeField] LayerMask Mask;
    [SerializeField] GameObject[] Hijos;
    bool confirmar = false;
    bool canplace, canplace1,canplace2 = true;
    public float Rotation;
    Renderer rend;
    [SerializeField] GameObject Pos1, Pos2, Pos3, Pos4 ,Tpose;
    [SerializeField] float H;
    [SerializeField] GameObject PrefabMesa;
    [SerializeField] bool casa, canrotate;
    public bool Confirmar { get { return confirmar; } }
    


    void Awake()
    {
        canrotate = true;
        canplace1 = true;
        canplace2 = true;
        mainCamera = Camera.main;
        if(casa)
        {
            azul = Hijos[0].GetComponent<Renderer>().material.color;
        }  
    }
    void Update()
    {
        Mouse3D();  // Detecto constantemente la posicion del mouse
        MoverBP();
        Inputs();
        if (confirmar && !canplace)
        {
            Destroy(gameObject);
        }
    }
    private void Mouse3D()
    {
        ray = mainCamera.ScreenPointToRay(Input.mousePosition);              // defino ray como el rayo que sale de la camara
        if (Physics.Raycast(ray, out Hit, 60, Mask))   // si apretan click mientras que rayo esta tocando 
        {
            PuntoHit = Hit.point;
        }
    }
    private void MoverBP()
    {
        if (!confirmar)
        {
            transform.position = PuntoHit;
            transform.rotation = Quaternion.Euler(transform.rotation.x, Rotation, transform.rotation.y);
        }
        
    }
    private void Inputs()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !confirmar)
        {
            Destroy(gameObject);
        }
        if (Input.GetKeyDown(KeyCode.Mouse1) && canplace)
        {
            confirmar = true;
            canrotate = false;
            transform.gameObject.tag = ("BluePrint");
        }
        if (canrotate)
        {
            if (Input.GetKey(KeyCode.R) && !Input.GetKey(KeyCode.LeftShift))
            {

                Rotation = Rotation + 90 * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.R) && Input.GetKey(KeyCode.LeftShift))
            {
                Rotation = Rotation - 90 * Time.deltaTime;
            }
        }
        
        if (Physics.Raycast(Pos1.transform.position, Vector3.down, H, Mask) && Physics.Raycast(Pos2.transform.position, Vector3.down, H, Mask) && Physics.Raycast(Pos3.transform.position, Vector3.down, H, Mask) && Physics.Raycast(Pos4.transform.position, Vector3.down, H, Mask))
        {
            canplace2 = true; 
        }
        else if (!Physics.Raycast(Pos1.transform.position, Vector3.down, H, Mask) || !Physics.Raycast(Pos2.transform.position, Vector3.down,H, Mask) || !Physics.Raycast(Pos3.transform.position, Vector3.down,H, Mask) || !Physics.Raycast( Pos4.transform.position, Vector3.down,H, Mask))
        {           
            canplace2 = false; 
        }
        if(canplace1 && canplace2)
        {
            canplace = true;
            if (casa)
            {
                for (int i = 0; i < 8; i++)
                {
                    Hijos[i].GetComponent<Renderer>().material.color = azul;
                    canplace2 = true;
                }
            }
            else
            {
                GetComponent<Renderer>().material.color = azul;
            }
        }
        else
        {
            canplace = false;
            if (casa)
            {
                for (int i = 0; i < 8; i++)
                {
                    Hijos[i].GetComponent<Renderer>().material.color = rojo;
                    canplace2 = false;
                }
            }
            else
            {
                GetComponent<Renderer>().material.color = rojo;
            }
            
        }

    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Obstaculo" || collision.gameObject.tag == "BluePrint" || collision.gameObject.tag == "Construct")
        {
            canplace1 = false;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        canplace1 = true;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(Pos1.transform.position, Vector3.down * H);
        Gizmos.DrawRay(Pos2.transform.position, Vector3.down * H);
        Gizmos.DrawRay(Pos3.transform.position, Vector3.down * H);
        Gizmos.DrawRay(Pos4.transform.position, Vector3.down * H);
    }
    public void Mesa()
    {
        if (casa)
        {
            Instantiate(PrefabMesa, Tpose.transform.position, Quaternion.Euler(0, Rotation + 180, 0));
        }
        else
        {
            Instantiate(PrefabMesa, Tpose.transform.position, Quaternion.Euler(0, this.Rotation + 0 , 0));
        }
        
        Destroy(gameObject);
    }

}

