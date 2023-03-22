using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Game_Pause.Global_Game_Pause.isPaused)
        {
                rb.isKinematic = true;
        }
        else
        {
                rb.isKinematic = false;
        }
    }
}
