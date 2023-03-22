using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SetFPS : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    public int SelectedOption;
    public int SelectedOptionValue;
    public int[] OptionValueList;

   

    void Awake()
    {
        if (dropdown == null) {
            dropdown = this.gameObject.GetComponent<TMP_Dropdown>();
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        TMP_Dropdown.OptionData[] options = dropdown.options.ToArray();
        OptionValueList = new int[options.Length];

        for (int i = 0; i < options.Length; i++)
        {
            OptionValueList[i] = int.Parse(options[i].text);
        }

        InitialSelectOption();
    }

    public void InitialSelectOption()
    {
        Debug.Log("inicializando FPSCap at " + Graficos.GlobalGameGraphics.actualGraphic.screenFPSCap);
        for (int i = 0; i < OptionValueList.Length; i++)
        {
            if(OptionValueList[i] == Graficos.GlobalGameGraphics.actualGraphic.screenFPSCap)
            {
                SelectedOption = i;
            }
        }
        //SelectedOption = OptionValueList  //Graficos.GlobalGameGraphics.actualGraphic.screenFPSCap;
        SelectedOptionValue = Graficos.GlobalGameGraphics.actualGraphic.screenFPSCap;
        setOption(SelectedOption);
    }

    public void SelectOption(int value)
    {
        SelectedOption = value;
        SelectedOptionValue = OptionValueList[SelectedOption];
        Graficos.GlobalGameGraphics.newGraphics.screenFPSCap = SelectedOptionValue;
    }
    public void setOption(int value)
    {
        Debug.Log("setOption");
        SelectedOption = value;
        dropdown.value = SelectedOption;
        SelectOption(value);
    }
}
