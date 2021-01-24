using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoFog : MonoBehaviour
{
    public bool AllowFog = false;

    private bool FogOn;

    private void OnPreRender()
    {
        FogOn = RenderSettings.fog;
        RenderSettings.fog = AllowFog;
    }

    private void OnPostRender()
    {
        RenderSettings.fog = FogOn;
    }
}
