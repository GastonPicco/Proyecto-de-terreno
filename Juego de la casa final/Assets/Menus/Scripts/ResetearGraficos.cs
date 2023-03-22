using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetearGraficos : MonoBehaviour
{
    public SetFPS setFPS;
    public SetResolutionD setResolutionD;
    public SetScreenType setScreenType;

    public SetScrollbar setScrollbar_Antialiasing;
    public SetScrollbar setScrollbar_Sombras;

    public SelectSound selectSound_Master;
    public SelectSound selectSound_Music;
    public SelectSound selectSound_Effects;
    public SelectSound selectSound_Ambient;

    // Start is called before the first frame update
    public void resetearGraficos()
    {
        Debug.Log("ReCargandoGraficos");
        //Graficos.GlobalGameGraphics.cargarGraficos();

        setFPS.InitialSelectOption();
        setResolutionD.InitialSelectOption();
        setScreenType.InitialSelectOption();
        setScrollbar_Antialiasing.InitialSelectOption();
        setScrollbar_Sombras.InitialSelectOption();
        selectSound_Master.InitialSelectOption();
        selectSound_Music.InitialSelectOption();
        selectSound_Effects.InitialSelectOption();
        selectSound_Ambient.InitialSelectOption();
    }

}
