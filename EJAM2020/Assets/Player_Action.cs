using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Action : MonoBehaviour
{
    Item it;

    private void Awake()
    {
        it = GetComponent<Item>();
    }

    private void Update()
    {
        if (it != null && it.OnPlayer)
        {
            if (Input.GetAxis("Fire") > 0)
            {
                JETIRE();
            }
        }
    }

    void JETIRE()
    {
        Debug.Log("JE TIRE " + gameObject.name);
    }
}
