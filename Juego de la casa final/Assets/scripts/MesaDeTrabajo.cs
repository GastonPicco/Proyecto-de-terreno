using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MesaDeTrabajo : MonoBehaviour
{
    [SerializeField] int Madera, MaxMadera;
    [SerializeField] int Piedra, MaxPiedra;
    [SerializeField] int Mineral, MaxMineral;
    [SerializeField] GameObject PrefabCasa , Casa;
    [SerializeField] GameObject[] Partes;
    [SerializeField] int Mat0, Mat1, Mat2, Mat3, Mat4, Mat5;
    public int mat0 { get { return Mat0; } }
    public int mat1 { get { return Mat1; } }
    public int mat2 { get { return Mat2; } }
    public int mat3 { get { return Mat3; } }
    public int mat4 { get { return Mat4; } }
    public int mat5 { get { return Mat5; } }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Mat0 = Madera;
        Mat1 = Piedra;
        Mat2 = Mineral;
        Mat3 = MaxMadera;
        Mat4 = MaxPiedra;
        Mat5 = MaxMineral;
        if (Madera > MaxMadera*0.2 && MaxMadera >= Madera)
        {
            Partes[0].SetActive(true);
            if (Madera > MaxMadera * 0.3)
            {
                Partes[2].SetActive(true);
                if (Madera > MaxMadera * 0.4)
                {
                    Partes[3].SetActive(true);
                    if (Madera > MaxMadera * 0.5)
                    {
                        Partes[4].SetActive(true);
                        if (Madera > MaxMadera * 0.6)
                        {
                            Partes[5].SetActive(true);
                            if (Madera > MaxMadera * 0.7)
                            {
                                Partes[6].SetActive(true);
                                if (Madera == MaxMadera)
                                {
                                    Partes[7].SetActive(true);
                                }
                            }
                        }
                    }
                }
            }
        }
        if (Piedra == MaxPiedra && Mineral == MaxMineral)
        {
            Partes[1].SetActive(true);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag + other);
        if (other.gameObject.tag == "madera" && Madera < MaxMadera)
        {
            Destroy(other.gameObject);
            Madera = Madera + 1;
        }
        if (other.tag=="piedra" && Piedra < MaxPiedra)
        {
            Destroy(other.gameObject);
            Piedra = Piedra + 1;
        }
        if (other.tag=="mineral" && Mineral < MaxMineral)
        {
            Destroy(other.gameObject);
            Mineral = Mineral + 1;
        }
        if(Mineral == MaxMineral && Madera == MaxMadera && Piedra == MaxPiedra)
        {
            Debug.Log("Completo");
            Instantiate(PrefabCasa,Casa.transform.position,Casa.transform.rotation);
            Destroy(gameObject);
        }

    }
    
}
