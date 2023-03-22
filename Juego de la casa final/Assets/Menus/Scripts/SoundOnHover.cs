using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOnHover : MonoBehaviour
{

    public AudioSource audioSource;
    
    public void PointerEnter()
    {
        audioSource.PlayOneShot(audioSource.clip);  
    }

}
