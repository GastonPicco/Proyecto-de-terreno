using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Pause : MonoBehaviour
{
    //se tiene a si mismo como unica instancia de Global_Game_Pause
    //este objeto es de tipo singleton, solo puede existir uno, de lo contrario,
    //queda el que fuera anterior, asi se evitan cosas raras, y se
    //consigue tener solo una instancia de este elemento
    public static Game_Pause Global_Game_Pause;

    //la variable global de pausa
    public bool isPaused;

    // Start is called before the first frame update
    void Awake()
    {
        //esto es para crear el objeto singletone, la logica es simple.
        //si no existia uno antes, se declara a si mismo como el objeto global
        //si ya existe otro se mata y si ya existe pero es el lo avisa por consola.

        if (Global_Game_Pause == null)
        {
            Debug.Log(this.gameObject.name + " (" + this.gameObject.GetInstanceID() + "): Ahora yo soy el Global_Game_Pause porque no habia otro");
            Global_Game_Pause = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else {
            if (Global_Game_Pause == this)
            {
                Debug.Log(this.gameObject.name + " (" + this.gameObject.GetInstanceID() + "): Yo soy el game Global_Game_Pause");
            }
            else {
                Debug.Log(this.gameObject.name + " (" + this.gameObject.GetInstanceID() + "): el GameObject " + Global_Game_Pause.gameObject.name + " (" + Global_Game_Pause.gameObject.GetInstanceID() + ") ya es el Global_Game_Pause, yo me voy a destruir, nos vemos...");
                Destroy(this.gameObject);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
