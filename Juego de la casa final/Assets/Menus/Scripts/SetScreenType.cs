using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SetScreenType : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    public int SelectedOption;
    public string SelectedOptionValue;
    public string[] OptionValueList;
    void Awake()
    {
        Debug.Log(dropdown);
        if (dropdown == null)
        {
            dropdown = this.gameObject.GetComponent<TMP_Dropdown>();
        }
        Debug.Log(dropdown);

    }

    // Start is called before the first frame update
    void Start()
    {
        TMP_Dropdown.OptionData[] options = dropdown.options.ToArray();
        OptionValueList = new string[options.Length];

        for (int i = 0; i < options.Length; i++)
        {
            OptionValueList[i] = options[i].text;
        }

        InitialSelectOption();
    }

    public void InitialSelectOption()
    {
        SelectedOption = Graficos.GlobalGameGraphics.actualGraphic.screenType;
        SelectedOptionValue = OptionValueList[SelectedOption];
        setOption(SelectedOption);
    }

    public void SelectOption(int value)
    {
        SelectedOption = value;
        SelectedOptionValue = OptionValueList[SelectedOption];
        Graficos.GlobalGameGraphics.newGraphics.screenType = SelectedOption;
    }

    public void setOption(int value)
    {
        Debug.Log("setOption");
        SelectedOption = value;
        dropdown.value = SelectedOption;
        SelectOption(value);
    }

}
