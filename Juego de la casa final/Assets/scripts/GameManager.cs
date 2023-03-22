using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager data;
    public GameObject Player;

    void Awake()
    {

        if (data == null)
        {
            Debug.Log(this.gameObject.name + ": Soy el nuevo GameManager");
            data = this;
        }
        else
        {

            if (data == this)
            {
                Debug.Log(this.gameObject.name + ": Yo ya era el GameManager...");
            }
            else
            {
                Debug.Log(this.gameObject.name + ": Ya hay otro encargandose de ser el GameManager, me hago la automoricion");
                Destroy(gameObject);
            }

        }

    }

}

//buscar al player, esto va en el enemigo
//jugador = GameManager.data.Player.transform.position;


