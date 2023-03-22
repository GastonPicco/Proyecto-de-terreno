using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controlador_De_Recursos : MonoBehaviour
{
    [SerializeField] GameObject Item;
    [Header("1-tree 2-roca 3-Mineral ")]
    [SerializeField] int tipo;
    Vector3 Desp = new Vector3(0,2.5f,0);


    void Start()
    {
        
    }

    
    void Update()
    {
        if (gameObject.transform.position.y < -40)
        {
            Destroy(gameObject);
        }
    }
    public void Ocultar()
    {
        if(tipo == 1)
        {
            Debug.Log("Instant");
            Instantiate(Item, transform.position + Desp, Quaternion.Euler(-35, 215, 49));
            Instantiate(Item, transform.position + Desp * 2, Quaternion.Euler(-55, 245, 49));
            Instantiate(Item, transform.position + Desp * 3, Quaternion.Euler(-55, 165, 49));
            Instantiate(Item, transform.position + Desp * 4, Quaternion.Euler(-55, 95, 49));
            gameObject.transform.Translate(0,-80,0);
        }
        if(tipo == 2)
        {
            Debug.Log("Instant");
            Instantiate(Item, transform.position + new Vector3(0f, 1.6f, 0.0f), Quaternion.Euler(-35, 215, 49));
            Instantiate(Item, transform.position + new Vector3(-0.8f, 0.25f, 0.8f), Quaternion.Euler(-55, 245, 49));
            Instantiate(Item, transform.position + new Vector3(0.8f, 0.25f, -0.8f), Quaternion.Euler(-55, 165, 49));
            Destroy(gameObject);
        }
        if (tipo == 3)
        {
            Debug.Log("Instant");
            Instantiate(Item, transform.position + new Vector3(0.5f, 0.5f, 0.5f), Quaternion.Euler(-35, 215, 49));
            Instantiate(Item, transform.position + new Vector3(-0.5f, 0.5f, -0.5f), Quaternion.Euler(-55, 245, 49));
            Destroy(gameObject);
        }

    }
}
