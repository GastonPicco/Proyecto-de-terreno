using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject slime;
    [SerializeField] float timer;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(slime, transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if (!Game_Pause.Global_Game_Pause.isPaused)
        {
            timer += Time.deltaTime;
            if (timer > 300)
            {
                Instantiate(slime, transform.position, Quaternion.identity);
                timer = 0;
            }
        }
    }
}
