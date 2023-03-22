using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lanza : MonoBehaviour
{
    [SerializeField] float velocidad,timerCaer;
    [SerializeField] bool col, quieto;
    Rigidbody Rb;
    // Start is called before the first frame update
    void Start()
    {
        timerCaer = 0;
        Rb = gameObject.GetComponent<Rigidbody>();
        quieto = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Game_Pause.Global_Game_Pause.isPaused)
        {
            timerCaer += Time.deltaTime;
            if (!col)
            {
                gameObject.transform.Translate(Vector3.forward * velocidad * Time.deltaTime);
            }
            if (timerCaer > 0.5 && !quieto)
            {
                gameObject.transform.Translate(Vector3.down * (velocidad / 6) * Time.deltaTime);
                if (timerCaer > 1 && !quieto)
                {
                    gameObject.transform.Translate(Vector3.down * (velocidad / 12) * Time.deltaTime);
                    if (timerCaer > 1.2f && !quieto)
                    {
                        gameObject.transform.Translate(Vector3.down * (velocidad / 24) * Time.deltaTime);
                    }
                }

            }
        }
            
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Obstaculo")
        {
            col = true;
            Rb.constraints = RigidbodyConstraints.FreezeAll;
            quieto = true;
            gameObject.layer = (13);
        }
        if (collision.gameObject.tag == "Enemy" && !quieto)
        {            
            col = true;
            Rb.constraints = RigidbodyConstraints.FreezeAll;
            collision.gameObject.transform.position = new Vector3(0, -10, 0);
            quieto = true;          
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Obstaculo" || collision.gameObject.tag == "Enemy")
        {
            Rb.constraints = RigidbodyConstraints.None;
        }
    }
}
