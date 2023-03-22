using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectGraphics : MonoBehaviour
{
    public Graficos graphicScript;
    public SetScrollbar setScrollbarScript;
    public SetFPS setFPS;
    public SetResolutionD setResolutionD;
    public SetScreenType setScreenType;
    public Scrollbar scrollbar;
    public TMP_Dropdown dropdown;

    private void Start()
    {
        graphicScript = Graficos.GlobalGameGraphics;

        switch (selectorType)
        {
            case SelectorType.Dropdown:
                dropdown = this.gameObject.GetComponent<TMP_Dropdown>();

                switch (typeScreenOpt)
                {
                    case TypeScreenOpt.ScreenType:
                        setScreenType = this.gameObject.GetComponent<SetScreenType>();
                        break;

                    case TypeScreenOpt.Resolution:
                        setResolutionD = this.gameObject.GetComponent<SetResolutionD>();
                        break;

                    case TypeScreenOpt.FPSCap:
                        setFPS = this.gameObject.GetComponent<SetFPS>();
                        break;
                }
                break;

            case SelectorType.Scrollbar:
                scrollbar = this.gameObject.GetComponent<Scrollbar>();
                setScrollbarScript = this.gameObject.GetComponent<SetScrollbar>();
                break;
        }
    }

    public enum SelectorType
    {
        Dropdown,
        Scrollbar,
        Slider
    }
    public SelectorType selectorType;

    public enum TypeGraphicsOpt
    {
        Sombra,
        Antialiasing
    }
    public TypeGraphicsOpt typeGraphicsOpt;

    public enum TypeScreenOpt
    {
        ScreenType,
        Resolution,
        FPSCap,
    }
    public TypeScreenOpt typeScreenOpt;

    public void setGraphics() {

        switch (selectorType)
        {
            case SelectorType.Dropdown:
                switch (typeScreenOpt)
                {
                    case TypeScreenOpt.ScreenType:
                        graphicScript.newGraphics.screenType = setScreenType.SelectedOption;
                        break;

                    case TypeScreenOpt.Resolution:
                        graphicScript.newGraphics.screenHeight = setResolutionD.resolutionSelected.height;
                        graphicScript.newGraphics.screenWidth = setResolutionD.resolutionSelected.width;
                        graphicScript.newGraphics.screenRefreshRate = setResolutionD.resolutionSelected.refreshRate;
                        break;

                    case TypeScreenOpt.FPSCap:
                        graphicScript.newGraphics.screenFPSCap = setFPS.SelectedOptionValue;
                        break;
                }
                break;

            case SelectorType.Scrollbar:
                //los scrollbar solo se configuraron para Sombra y Antialiasing
                switch (typeGraphicsOpt)
                {
                    case TypeGraphicsOpt.Sombra:
                        //Debug.Log(this.gameObject.name);
                        //Debug.Log(setScrollbarScript.gameObject.name);
                        //Debug.Log(setScrollbarScript);
                        //Debug.Log(setScrollbarScript.SelectedOption);
                        graphicScript.newGraphics.shadowSetings = setScrollbarScript.SelectedOption;
                        break;

                    case TypeGraphicsOpt.Antialiasing:
                        graphicScript.newGraphics.AntialiasingSetings = setScrollbarScript.SelectedOption;
                        break;
                }
                break;

            case SelectorType.Slider:
                break;
        }
        //graphicScript.newGraphics
        graphicScript.checkChangesInGraphics();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
