using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsButton : MonoBehaviour
{
    public Animator menuAnimator;
    public string variableACambiar;


    public void ChangeAnimatorBoolValue(bool value)
    {
        menuAnimator.SetBool(variableACambiar, value);
    }

    public void InvertAnimatorBoolValue()
    {
        menuAnimator.SetBool(variableACambiar, !menuAnimator.GetBool(variableACambiar) );
    }

    public void PlaySoundOnce(AudioSource audioSource)
    {
        audioSource.PlayOneShot(audioSource.clip);
    }
}
