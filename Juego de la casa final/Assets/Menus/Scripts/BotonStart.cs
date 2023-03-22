using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class BotonStart : MonoBehaviour
{
    public string Nivel;
    public string Menu;

    public Animator menuAnimator;
    public string varAbrirMenu;
    public string varMenuPrincipal;

    public bool comenzoCargaDeNivel;
    public float contadorFundido = 0f;
    public float tiempoHastaFundido = 20f;
    public float porcentajeFundido = 0f;

    public bool comenzoDesCargaDeNivel;
    public float contadorFundidoOpuesto = 0f;
    public float tiempoHastaFundidoOpuesto = 20f;
    public float porcentajeFundidoOpuesto = 0f;


    public Color color1;
    public Color color2;
    public Color colorActual;
    public Image imagenFundido;

    // Update is called once per frame
    void Update()
    {
        if (comenzoCargaDeNivel)
        {
            if (contadorFundido < tiempoHastaFundido)
            {
                contadorFundido += 1 * Time.deltaTime;
                porcentajeFundido = contadorFundido / tiempoHastaFundido;
                colorActual.r = Mathf.Lerp(color1.r, color2.r, porcentajeFundido);
                colorActual.g = Mathf.Lerp(color1.g, color2.g, porcentajeFundido);
                colorActual.b = Mathf.Lerp(color1.b, color2.b, porcentajeFundido);
                colorActual.a = Mathf.Lerp(color1.a, color2.a, porcentajeFundido);
                imagenFundido.color = colorActual;
            }
            else
            {

                if (SceneManager.GetActiveScene().name == Menu)
                {
                    contadorFundido = 0;
                    comenzoDesCargaDeNivel = true;
                    comenzoCargaDeNivel = false;
                    SceneManager.LoadScene(Nivel, LoadSceneMode.Single);
                }

                if (SceneManager.GetActiveScene().name == Nivel)
                {
                    contadorFundido = 0;
                    comenzoDesCargaDeNivel = true;
                    comenzoCargaDeNivel = false;
                    menuAnimator.SetBool(varMenuPrincipal, true);
                    SceneManager.LoadScene(Menu, LoadSceneMode.Single);
                }

                
            }
        }

        if (comenzoDesCargaDeNivel)
        {
            if (contadorFundidoOpuesto < tiempoHastaFundidoOpuesto)
            {
                contadorFundidoOpuesto += 1 * Time.deltaTime;
                porcentajeFundidoOpuesto = contadorFundidoOpuesto / tiempoHastaFundidoOpuesto;
                colorActual.r = Mathf.Lerp(color2.r, color1.r, porcentajeFundidoOpuesto);
                colorActual.g = Mathf.Lerp(color2.g, color1.g, porcentajeFundidoOpuesto);
                colorActual.b = Mathf.Lerp(color2.b, color1.b, porcentajeFundidoOpuesto);
                colorActual.a = Mathf.Lerp(color2.a, color1.a, porcentajeFundidoOpuesto);
                imagenFundido.color = colorActual;
            }
            else
            {
                if (SceneManager.GetActiveScene().name == Menu)
                {
                    menuAnimator.SetBool(varMenuPrincipal, true);
                    menuAnimator.SetBool(varAbrirMenu, true);
                }
                contadorFundidoOpuesto = 0;
                comenzoDesCargaDeNivel = false;
            }
        }
    }

    public void CargarNivel(string nombreNivel){
        comenzoCargaDeNivel = true;
        menuAnimator.SetBool(varAbrirMenu, !menuAnimator.GetBool(varAbrirMenu));
        
    }
}
