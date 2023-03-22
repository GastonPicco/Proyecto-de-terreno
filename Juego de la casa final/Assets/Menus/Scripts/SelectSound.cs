using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectSound : MonoBehaviour
{
    public enum SoundType { 
        Master,
        Music,
        Ambient,
        Effects
    }

    public SoundType soundType;
    public Graficos graphicScript;
    public Change_AudioMixer change_AudioMixerScript;

    // Start is called before the first frame update
    void Start()
    {
        change_AudioMixerScript = this.gameObject.GetComponentInChildren<Change_AudioMixer>();
        graphicScript = Graficos.GlobalGameGraphics;

        InitialSelectOption();
    }

    public void InitialSelectOption()
    {

        switch (soundType)
        {
            case SoundType.Master:
                change_AudioMixerScript.scrollbar.value = (float)graphicScript.actualGraphic.volumeMaster / 100;
                change_AudioMixerScript.updateText(graphicScript.actualGraphic.volumeMaster);
                break;

            case SoundType.Ambient:
                change_AudioMixerScript.scrollbar.value = (float)graphicScript.actualGraphic.volumeAmbientSound / 100;
                change_AudioMixerScript.updateText(graphicScript.actualGraphic.volumeAmbientSound);
                break;

            case SoundType.Music:
                change_AudioMixerScript.scrollbar.value = (float)graphicScript.actualGraphic.volumeMusic / 100;
                change_AudioMixerScript.updateText(graphicScript.actualGraphic.volumeMusic);
                break;

            case SoundType.Effects:
                change_AudioMixerScript.scrollbar.value = (float)graphicScript.actualGraphic.volumeSoundEfects / 100;
                change_AudioMixerScript.updateText(graphicScript.actualGraphic.volumeSoundEfects);
                break;
        }

        //change_AudioMixerScript.SetVolumeOnMixerScrollbarV();
        graphicScript.checkChangesInGraphics();
        
        //change_AudioMixerScript.scrollbar.value = 


        //Debug.Log("inicializando FPSCap at " + Graficos.GlobalGameGraphics.actualGraphic.screenFPSCap);
        //for (int i = 0; i < OptionValueList.Length; i++)
        //{
        //    if (OptionValueList[i] == Graficos.GlobalGameGraphics.actualGraphic.screenFPSCap)
        //    {
        //        SelectedOption = i;
        //    }
        //}
        ////SelectedOption = OptionValueList  //Graficos.GlobalGameGraphics.actualGraphic.screenFPSCap;
        //SelectedOptionValue = Graficos.GlobalGameGraphics.actualGraphic.screenFPSCap;
        //setOption(SelectedOption);
    }

    public void setGraphics()
    {
        switch (soundType)
        {
            case SoundType.Master:
                graphicScript.newGraphics.volumeMaster = Mathf.RoundToInt(change_AudioMixerScript.linearNumber);
                break;

            case SoundType.Ambient:
                graphicScript.newGraphics.volumeAmbientSound = Mathf.RoundToInt(change_AudioMixerScript.linearNumber);
                break;

            case SoundType.Music:
                graphicScript.newGraphics.volumeMusic = Mathf.RoundToInt(change_AudioMixerScript.linearNumber);
                break;

            case SoundType.Effects:
                graphicScript.newGraphics.volumeSoundEfects = Mathf.RoundToInt(change_AudioMixerScript.linearNumber);
                break;
        }

        graphicScript.checkChangesInGraphics();
    }
}
