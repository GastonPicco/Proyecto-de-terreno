using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity;

public class CloseGame : MonoBehaviour
{
   
    public void CloseGameVoid()
    {
        Debug.Log("Closing Game...");
        Application.Quit();
    }

}
