using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_knife : MonoBehaviour
{
    Player_Movement PM;

    Item it;
    [Range(0,3)]
    public float TimeMax;
    float time;
    bool ok = true;

    private void Awake()
    {
        it = GetComponent<Item>();
        PM = Player_Movement.Instance;
    }

    private void Update()
    {
        if (it != null && it.OnPlayer)
        {
            if (Input.GetAxis("Fire") > 0 && ok)
            {
                JETIRE();
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
        RaycastHit hit;
        Debug.DrawRay(it.Player.position, it.Player.forward);
        if (Physics.Raycast(it.Player.position, it.Player.forward, out hit, 1f))
        {
            if (hit.collider.GetComponent<IAMovement>() != null)
            {
                CameraShake.Instance.ShakeIt();
                hit.collider.GetComponent<IAMovement>().Hited(transform.position, true);
            }
        }

        PM.myAnimator.SetTrigger("Knife");
    }

}
