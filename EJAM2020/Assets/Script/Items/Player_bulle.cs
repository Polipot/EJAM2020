using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Player_bulle : MonoBehaviour
{
    Player_Movement PM;
    Item it;
    public LayerMask theLayerMask;

    [Range(0, 2)]
    public float ForceMax;
    [Range(0, 3)]
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
            if (!ok)
            {
                time += Time.deltaTime;

                if (time >= TimeMax)
                {
                    ok = true;
                }
            }

            if (Input.GetAxis("Fire") > 0 && ok)
            {
                JETIRE();
                time = 0;
                ok = false;
            }
        }
    }

    void JETIRE()
    {
        var bb = (GameObject)Instantiate(Resources.Load("Particles/Particle_bulle"), it.Player.position, it.Player.rotation);
        bb.transform.SetParent(it.Player.transform);
        Destroy(bb, 5f);
        AddObjectSound();

        RaycastHit[] hit = Physics.RaycastAll(it.Player.position, it.Player.forward, 4f, theLayerMask).OrderBy(h => h.distance).ToArray();

        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].collider.tag.Equals("Wall"))
            {
                break;
            }

            else if (hit[i].collider.GetComponent<CourtCircuit>())
            {
                hit[i].collider.GetComponent<CourtCircuit>().Activation();
                break;
            }

            else if (hit[i].collider.GetComponent<IAMovement>() != null)
            {
                CameraShake.Instance.ShakeIt();

                Vector3 dir = (hit[i].collider.transform.position - transform.position).normalized;

                hit[i].collider.GetComponent<IAMovement>().Hited(transform.position, false, true, false);

                if (hit[i].collider.GetComponent<Rigidbody>() != null)
                {
                    hit[i].collider.GetComponent<Rigidbody>().AddForce(dir * ForceMax * 10, ForceMode.Impulse);
                }
            }
        }

        PM.myAnimator.SetTrigger("Tir");
    }

    void AddObjectSound()
    {
        var O_sound = (GameObject)Instantiate(Resources.Load("Prefabs/SonBulles"), transform.position, transform.rotation);
    }
}
