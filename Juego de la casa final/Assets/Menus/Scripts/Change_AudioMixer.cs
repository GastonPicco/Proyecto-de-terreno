using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class Change_AudioMixer : MonoBehaviour
{
    public Slider slider;
    public Scrollbar scrollbar;

    public TMP_Text texto;
    public AudioMixer audioMixer;
    public AudioMixerGroup CanalDeSonidoAManipular;

    [Range(0,100)]
    public int linearNumber;
    public float logaritmicNumber;

    // Start is called before the first frame update
    void Start()
    {
        //if (slider != null) {
        //    SetVolumeOnMixerSlider(slider.value);
        //}

        //if (scrollbar != null)
        //{
        //    SetVolumeOnMixerScrollbar(scrollbar.value);
        //}

 
    }

    public void SetVolumeOnMixerScrollbarV()
    {
        linearNumber = Mathf.RoundToInt(scrollbar.value * 100);
        if (texto != null)
        {
            texto.text = linearNumber.ToString();
        }
        logaritmicNumber = LinearToDecibel(scrollbar.value * 100);
        audioMixer.SetFloat(CanalDeSonidoAManipular.name, logaritmicNumber);
    }

    public void updateText(int valueNumber)
    {
        if (texto != null)
        {
            texto.text = valueNumber.ToString();
        }
    }

    public void SetVolumeOnMixerScrollbar(float scrollbarValue)
    {
        linearNumber = Mathf.RoundToInt(scrollbarValue * 100);
        if (texto != null)
        {
            texto.text = linearNumber.ToString();
        }
        logaritmicNumber = LinearToDecibel(scrollbarValue * 100);
        audioMixer.SetFloat(CanalDeSonidoAManipular.name, logaritmicNumber);
    }

    public float LinearToDecibel(float linear)
     {
         float dB;
       
         if (linear != 0)
             dB = 20.0f * Mathf.Log10(linear/10);
         else
             dB = -144.0f;
       
         return dB;
     }
}
