using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debug_SoundOnClick : MonoBehaviour
{
    public AudioSource Audio_source;
    public float play = 1.4f;
    public float stop = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Audio_source.isPlaying)
        {
            this.gameObject.transform.localScale = new Vector3(play, play, play);
        }
        else
        {
            this.gameObject.transform.localScale = new Vector3(stop, stop, stop);

        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                //Select stage    
                if (hit.transform.name == this.gameObject.name)
                {
                    if (Audio_source.isPlaying) {
                        Audio_source.Stop();
                    }
                    else
                    {
                        Audio_source.Play();
                    }
                }
            }
        }
    }
}
