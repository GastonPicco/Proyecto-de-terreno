using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscPause : MonoBehaviour
{
    public Animator animatorGui;
    public string boolString;

    public string nombreEscenaMenuPrincipal;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(SceneManager.GetActiveScene().name != nombreEscenaMenuPrincipal)
            {
                animatorGui.SetBool(boolString, !animatorGui.GetBool(boolString));
            }
            
        }
    }
}
