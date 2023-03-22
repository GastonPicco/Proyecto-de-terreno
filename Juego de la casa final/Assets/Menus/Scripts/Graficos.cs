using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GraphicOptions
{

    public int screenWidth;
    public int screenHeight;
    public int screenRefreshRate;

    public int screenFPSCap;

    //tipo pantalla, 0 = ventana, 1 = Borderless, 2 = Fullscreen
    public int screenType;

    //tipo pantalla, 0 = off, 1 = bajo, 2 = medio, 3 = alto
    public int shadowSetings;

    //tipo pantalla, 0 = off, 1 = FXAA, 2 = SMAA
    public int AntialiasingSetings;

    //-------------------------------------------------------
    public int volumeMaster;
    public int volumeMusic;
    public int volumeAmbientSound;
    public int volumeSoundEfects;

    
}

public class Graficos : MonoBehaviour
{
    public GameObject baseObject;
    public static Graficos GlobalGameGraphics;
    public Resoluciones resolucionesScript;

    public bool HayCambios;

    public GraphicOptions actualGraphic;
    public GraphicOptions newGraphics;
    public GraphicOptions DefaultGraphics;
    public GraphicOptions FileGraphics;


    // Start is called before the first frame update
    void Awake()
    {
        if(GlobalGameGraphics == null)
        {
            Debug.Log("Yo sere el Script de Graficos.");
            GlobalGameGraphics = this;
        }
        else
        {
            if (GlobalGameGraphics != this)
            {
                Debug.Log("Existe un Script de Graficos Funcionando y no soy yo, me retiro...");
                Destroy(baseObject);
            }
            else 
            {
                Debug.Log("Ya soy el Script de Graficos.");
            }
        }

        newGraphics = FileGraphics;
        //cargarGraficos();

    }

    private void Start()
    {
        cargarGraficos();
    }

    // Update is called once per frame
    public void checkChangesInGraphics()
    {
        HayCambios = !identical(actualGraphic, newGraphics);
    }

    public bool identical(GraphicOptions firstOBJ, GraphicOptions secondOBJ)
    {

        int diferente = 0;

        if (firstOBJ.screenWidth != secondOBJ.screenWidth) { diferente++; }
        if (firstOBJ.screenHeight != secondOBJ.screenHeight) { diferente++; }
        if (firstOBJ.screenRefreshRate != secondOBJ.screenRefreshRate) { diferente++; }
        if (firstOBJ.screenType != secondOBJ.screenType) { diferente++; }
        if (firstOBJ.screenFPSCap != secondOBJ.screenFPSCap) { diferente++; }
        if (firstOBJ.shadowSetings != secondOBJ.shadowSetings) { diferente++; }
        if (firstOBJ.AntialiasingSetings != secondOBJ.AntialiasingSetings) { diferente++; }
        if (firstOBJ.volumeMaster != secondOBJ.volumeMaster) { diferente++; }
        if (firstOBJ.volumeMusic != secondOBJ.volumeMusic) { diferente++; }
        if (firstOBJ.volumeAmbientSound != secondOBJ.volumeAmbientSound) { diferente++; }
        if (firstOBJ.volumeSoundEfects != secondOBJ.volumeSoundEfects) { diferente++; }

        return diferente == 0;
    }

    public void copyData(GraphicOptions Origin, GraphicOptions Destination)
    {
        Destination.screenWidth = Origin.screenWidth;
        Destination.screenHeight = Origin.screenHeight;
        Destination.screenRefreshRate = Origin.screenRefreshRate;
        Destination.screenFPSCap = Origin.screenFPSCap;
        Destination.screenType = Origin.screenType;
        Destination.shadowSetings = Origin.shadowSetings;
        Destination.AntialiasingSetings = Origin.AntialiasingSetings;
        Destination.volumeMaster = Origin.volumeMaster;
        Destination.volumeMusic = Origin.volumeMusic;
        Destination.volumeAmbientSound = Origin.volumeAmbientSound;
        Destination.volumeSoundEfects = Origin.volumeSoundEfects;
    }

    public void guardarGraficos()
    {
        PlayerPrefs.SetInt("screenWidth", actualGraphic.screenWidth);
        PlayerPrefs.SetInt("screenHeight", actualGraphic.screenHeight);
        PlayerPrefs.SetInt("screenRefreshRate", actualGraphic.screenRefreshRate);

        PlayerPrefs.SetInt("screenFPSCap", actualGraphic.screenFPSCap);

        PlayerPrefs.SetInt("screenType", actualGraphic.screenType);

        PlayerPrefs.SetInt("shadowSetings", actualGraphic.shadowSetings);

        PlayerPrefs.SetInt("AntialiasingSetings", actualGraphic.AntialiasingSetings);

        PlayerPrefs.SetInt("volumeMaster", actualGraphic.volumeMaster);
        PlayerPrefs.SetInt("volumeMusic", actualGraphic.volumeMusic);
        PlayerPrefs.SetInt("volumeAmbientSound", actualGraphic.volumeAmbientSound);
        PlayerPrefs.SetInt("volumeSoundEfects", actualGraphic.volumeSoundEfects);

        PlayerPrefs.Save();
    }

    public void cargarGraficos()
    {
        HayCambios = true;

        newGraphics.screenWidth = PlayerPrefs.GetInt("screenWidth", DefaultGraphics.screenWidth);
        newGraphics.screenHeight = PlayerPrefs.GetInt("screenHeight", DefaultGraphics.screenHeight);
        newGraphics.screenRefreshRate = PlayerPrefs.GetInt("screenRefreshRate", DefaultGraphics.screenRefreshRate);

        newGraphics.screenFPSCap = PlayerPrefs.GetInt("screenFPSCap", DefaultGraphics.screenFPSCap);

        newGraphics.screenType = PlayerPrefs.GetInt("screenType", DefaultGraphics.screenType);

        newGraphics.shadowSetings = PlayerPrefs.GetInt("shadowSetings", DefaultGraphics.shadowSetings);

        newGraphics.AntialiasingSetings = PlayerPrefs.GetInt("AntialiasingSetings", DefaultGraphics.AntialiasingSetings);

        newGraphics.volumeMaster = PlayerPrefs.GetInt("volumeMaster", DefaultGraphics.volumeMaster);
        newGraphics.volumeMusic = PlayerPrefs.GetInt("volumeMusic", DefaultGraphics.volumeMusic);
        newGraphics.volumeAmbientSound = PlayerPrefs.GetInt("volumeAmbientSound", DefaultGraphics.volumeAmbientSound);
        newGraphics.volumeSoundEfects = PlayerPrefs.GetInt("volumeSoundEfects", DefaultGraphics.volumeSoundEfects);

        setOptionsControlers();
        setNewGraphics();
    }

    public void setOptionsControlers() {
        //setOption(int value)
    }
    public void setNewGraphics()
    {
        if (HayCambios == true)
        {
            Debug.Log("Actualizando Graficos");
            //resolucion
            FullScreenMode tipoVentana = FullScreenMode.Windowed;
            if(newGraphics.screenType == 1) { tipoVentana = FullScreenMode.FullScreenWindow; }
            if (newGraphics.screenType == 2) { tipoVentana = FullScreenMode.ExclusiveFullScreen; }
            Screen.SetResolution(newGraphics.screenWidth, newGraphics.screenHeight, tipoVentana, newGraphics.screenRefreshRate);
            Debug.Log("se cambio la resolucion a " + newGraphics.screenWidth + "x" + newGraphics.screenHeight + " " + newGraphics.screenRefreshRate + "Hz y " + tipoVentana);

            if (newGraphics.screenFPSCap != newGraphics.screenRefreshRate)
            {
                Debug.Log("se cambiaron los fps a " + newGraphics.screenFPSCap + " y se activo el VSync");
                QualitySettings.vSyncCount = 0;
                Application.targetFrameRate = newGraphics.screenFPSCap;
            }
            else {
                Debug.Log("se cambiaron los fps a " + newGraphics.screenFPSCap);
                QualitySettings.vSyncCount = 1;
                Application.targetFrameRate = newGraphics.screenFPSCap;
            }

            switch (newGraphics.AntialiasingSetings)
            {
                case 0:
                    Debug.Log("se cambiaron las sombras a Off");
                    UnityGraphicsBullshit.MSAA_Quality = UnityEngine.Rendering.Universal.MsaaQuality.Disabled;
                    break;

                case 1:
                    Debug.Log("se cambio el antialiasing a MSAAx2");
                    UnityGraphicsBullshit.MSAA_Quality = UnityEngine.Rendering.Universal.MsaaQuality._2x;
                    break;

                case 2:
                    Debug.Log("se cambio el antialiasing a MSAAx4");
                    UnityGraphicsBullshit.MSAA_Quality = UnityEngine.Rendering.Universal.MsaaQuality._4x;
                    break;

                case 3:
                    Debug.Log("se cambio el antialiasing a MSAAx8");
                    UnityGraphicsBullshit.MSAA_Quality = UnityEngine.Rendering.Universal.MsaaQuality._8x;
                    break;

            }

            switch (newGraphics.shadowSetings)
            {
                case 0:
                    Debug.Log("se cambiaron las sombras a Off");

                    UnityGraphicsBullshit.MainLightCastShadows = false;

                    QualitySettings.shadows = ShadowQuality.Disable;
                    QualitySettings.shadowResolution = ShadowResolution.Low;
                    break;

                case 1:
                    Debug.Log("se cambiaron las sombras a Bajo");

                    UnityGraphicsBullshit.MainLightCastShadows = true;
                    UnityGraphicsBullshit.MainLightShadowResolution = UnityEngine.Rendering.Universal.ShadowResolution._512;

                    QualitySettings.shadows = ShadowQuality.HardOnly;
                    QualitySettings.shadowResolution = ShadowResolution.Medium;
                    break;

                case 2:
                    Debug.Log("se cambiaron las sombras a Medio");

                    UnityGraphicsBullshit.MainLightCastShadows = true;
                    UnityGraphicsBullshit.MainLightShadowResolution = UnityEngine.Rendering.Universal.ShadowResolution._1024;

                    QualitySettings.shadows = ShadowQuality.All;
                    QualitySettings.shadowResolution = ShadowResolution.Medium;
                    break;

                case 3:
                    Debug.Log("se cambiaron las sombras a Alto");

                    UnityGraphicsBullshit.MainLightCastShadows = true;
                    UnityGraphicsBullshit.MainLightShadowResolution = UnityEngine.Rendering.Universal.ShadowResolution._2048;

                    QualitySettings.shadows = ShadowQuality.All;
                    QualitySettings.shadowResolution = ShadowResolution.High;
                    break;

                case 4:
                    Debug.Log("se cambiaron las sombras a Ultra");

                    UnityGraphicsBullshit.MainLightCastShadows = true;
                    UnityGraphicsBullshit.MainLightShadowResolution = UnityEngine.Rendering.Universal.ShadowResolution._4096;

                    QualitySettings.shadows = ShadowQuality.All;
                    QualitySettings.shadowResolution = ShadowResolution.VeryHigh;
                    break;
            }
            
            Debug.Log("SE ACTUALIZARON LOS GRAFICOS");

            copyData(newGraphics, actualGraphic);
            guardarGraficos();
            HayCambios = false;
        }
    }
}
