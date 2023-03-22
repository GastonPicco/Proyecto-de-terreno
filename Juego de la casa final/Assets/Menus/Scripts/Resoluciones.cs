using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class ResolucionesCustom
{
    public int width;
    public int height;
    public int refreshRate;
}

public class Resoluciones : MonoBehaviour
{

    //public TMP_Dropdown dropDownResolutions;

    public int selectedResolution = -1;
    public ResolucionesCustom selectedResolutionData;

    public Resolution[] resolutions;
    public ResolucionesCustom[] resolutionsDebug;

    // Start is called before the first frame update
    private void Awake()
    {
        resolutions = Screen.resolutions;
        resolutionsDebug = new ResolucionesCustom[resolutions.Length];

        //dropDownResolutions.options.Clear();

        for (int i = 0; i < resolutions.Length; i++)
        {

            resolutionsDebug[i] = new ResolucionesCustom();
            resolutionsDebug[i].width = resolutions[i].width;
            resolutionsDebug[i].height = resolutions[i].height;
            resolutionsDebug[i].refreshRate = resolutions[i].refreshRate;
        }
    }

    public void debugResolution(int value) {
        selectedResolution = value;
        selectedResolutionData.width = resolutionsDebug[selectedResolution].width;
        selectedResolutionData.height = resolutionsDebug[selectedResolution].height;
        selectedResolutionData.refreshRate = resolutionsDebug[selectedResolution].refreshRate;
    }

        // Update is called once per frame
        void Update()
    {
        
    }
}
