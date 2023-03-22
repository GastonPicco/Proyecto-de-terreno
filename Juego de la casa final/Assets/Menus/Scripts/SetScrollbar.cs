using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class TextoSliders
{
    public int valueInSlider;
    public string TextToShow;
}

public class SetScrollbar : MonoBehaviour
{
    public bool isShadow;
    public TMP_Text texto;
    public Scrollbar scrollbar;

    public int SelectedOption = -1;
    public int debug = -1;

    public int debugDefault = -1;

    public TextoSliders[] TextosSliders;
    // Start is called before the first frame update
    void Start()
    {
        if (scrollbar == null) {
            scrollbar = this.gameObject.GetComponent<Scrollbar>();
        }

        InitialSelectOption();
    }

    public void InitialSelectOption()
    {
        //Debug.Log("inicializando Resolution at " + Graficos.GlobalGameGraphics.actualGraphic.screenHeight + "p");

        if (isShadow)
        {
            SelectedOption = Graficos.GlobalGameGraphics.actualGraphic.shadowSetings;
        }
        else
        {
            SelectedOption = Graficos.GlobalGameGraphics.actualGraphic.AntialiasingSetings;
        }

        setOption(SelectedOption);
    }

    public void setOption(int value)
    {
        //Debug.Log("setOption");
        SelectedOption = value;
        scrollbar.value = (float)TextosSliders[SelectedOption].valueInSlider/100;
        SetTextSliderScrollbar( (float)TextosSliders[SelectedOption].valueInSlider/100 );
    }

    public void SetTextSliderScrollbar(float scrollbarValue)
    {
        int roundedValue = Mathf.RoundToInt(scrollbarValue * 100);
        debug = roundedValue;
        for (int i = 0; i < TextosSliders.Length; i++)
        {
            //reviso cada elemento de la lista de graficos
            if (TextosSliders[i].valueInSlider == roundedValue) {
                texto.text = TextosSliders[i].TextToShow;
                SelectedOption = i;
            }
        }
    }

    
}
