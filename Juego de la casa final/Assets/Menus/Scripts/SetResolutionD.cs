using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SetResolutionD : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    public int SelectedOption;
    public string SelectedOptionValue;
    public ResolucionesCustom resolutionSelected;
    public string[] OptionValueList;

    void Awake()
    {
        if (dropdown == null)
        {
            dropdown = this.gameObject.GetComponent<TMP_Dropdown>();
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        dropdown.options.Clear();
        for (int i = 0; i < Graficos.GlobalGameGraphics.resolucionesScript.resolutionsDebug.Length; i++){
            dropdown.options.Add(new TMP_Dropdown.OptionData(Graficos.GlobalGameGraphics.resolucionesScript.resolutionsDebug[i].width + "x" + Graficos.GlobalGameGraphics.resolucionesScript.resolutionsDebug[i].height + " " + Graficos.GlobalGameGraphics.resolucionesScript.resolutionsDebug[i].refreshRate + "Hz"));
        }

        TMP_Dropdown.OptionData[] options = dropdown.options.ToArray();
        OptionValueList = new string[options.Length];

        for (int i = 0; i < options.Length; i++)
        {
            OptionValueList[i] = options[i].text;
        }

        InitialSelectOption();
        //setOption(SelectedOption);
    }

    public void InitialSelectOption()
    {
        Debug.Log("inicializando Resolution at " + Graficos.GlobalGameGraphics.actualGraphic.screenHeight + "p");

        for (int i = 0; i < Graficos.GlobalGameGraphics.resolucionesScript.resolutionsDebug.Length; i++)
        {
            if(Graficos.GlobalGameGraphics.resolucionesScript.resolutionsDebug[i].height == Graficos.GlobalGameGraphics.actualGraphic.screenHeight)
            {
                if (Graficos.GlobalGameGraphics.resolucionesScript.resolutionsDebug[i].width == Graficos.GlobalGameGraphics.actualGraphic.screenWidth)
                {
                    if (Graficos.GlobalGameGraphics.resolucionesScript.resolutionsDebug[i].refreshRate == Graficos.GlobalGameGraphics.actualGraphic.screenRefreshRate)
                    {
                        SelectedOption = i;
                    }
                }
            }
        }

        setOption(SelectedOption);
    }

    public void SelectOption(int value)
    {
        SelectedOption = value;
        SelectedOptionValue = OptionValueList[SelectedOption];
        resolutionSelected = Graficos.GlobalGameGraphics.resolucionesScript.resolutionsDebug[SelectedOption];
        Graficos.GlobalGameGraphics.newGraphics.screenHeight = resolutionSelected.height;
        Graficos.GlobalGameGraphics.newGraphics.screenWidth = resolutionSelected.width;
        Graficos.GlobalGameGraphics.newGraphics.screenRefreshRate = resolutionSelected.refreshRate;
    }

    public void setOption(int value)
    {
        Debug.Log("setOption");
        SelectedOption = value;
        dropdown.value = SelectedOption;
        Graficos.GlobalGameGraphics.resolucionesScript.debugResolution(SelectedOption);
        SelectOption(value);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
