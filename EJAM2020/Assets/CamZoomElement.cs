using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamZoomElement : MonoBehaviour
{
    bool first = false;

    private void Update()
    {
        if (IAManager.Instance.PolicemanOnGround && !first)
        {
            first = true;
        }
    }

    void zoomPolice()
    {
       // transform.position = 
    }
}
