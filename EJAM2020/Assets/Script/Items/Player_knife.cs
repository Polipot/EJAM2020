﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_knife : MonoBehaviour
{
    CameraShaker CS;
    Player_Movement PM;
    public BoxCollider bc;
    public BoxCollider bcItem;

    Item it;
    [Range(0,3)]
    public float TimeMax;
    float time;
    bool ok = true;

    private void Awake()
    {
        it = GetComponent<Item>();
        PM = Player_Movement.Instance;
        CS = CameraShaker.Instance;
    }

    private void Update()
    {

        if (it != null && it.OnPlayer)
        {
            if (Input.GetAxis("Fire") > 0 && ok)
            {
                PM.myAnimator.SetTrigger("Knife");
                bc.enabled = true;
                Invoke("JETIRE", 0.05f);
                time = 0;
                ok = false;
            }

            else if (!ok)
            {
                time += Time.deltaTime;

                if (time >= TimeMax)
                {
                    ok = true;
                }
            }
        }     
    }

    void JETIRE()
    {
        bc.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<IAMovement>() != null && it.Player != null)
        {
            CS.CameraShake();
            other.GetComponent<IAMovement>().Hited(transform.position, true);
        }
    }

}
