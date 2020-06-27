using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_bulle : MonoBehaviour
{
    Item it;

    [Range(0, 2)]
    public float ForceMax;
    [Range(0, 3)]
    public float TimeMax;
    float time;
    bool ok = true;

    private void Awake()
    {
        it = GetComponent<Item>();
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

        RaycastHit hit;
        Debug.DrawRay(it.Player.position, it.Player.forward);
        if (Physics.Raycast(it.Player.position, it.Player.forward, out hit, 2f))
        {
            if (hit.collider.GetComponent<IAMovement>() != null)
            {
                Vector3 dir = (hit.collider.transform.position - transform.position).normalized;

                hit.collider.GetComponent<IAMovement>().Hited(transform.position, false);

                if (hit.collider.GetComponent<Rigidbody>() != null)
                {
                    hit.collider.GetComponent<Rigidbody>().AddForce(dir * ForceMax, ForceMode.Impulse);
                }
            }
        }
    }
}
