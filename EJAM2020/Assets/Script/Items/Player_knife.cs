using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_knife : MonoBehaviour
{
    Item it;
    [Range(0,3)]
    public float TimeMax;
    float time;
    bool ok;

    private void Awake()
    {
        it = GetComponent<Item>();
    }

    private void Update()
    {
        if (it != null && it.OnPlayer)
        {
            time += Time.deltaTime;

            if (time >= TimeMax)
            {
                ok = true;              
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
        Debug.Log("JE TIRE " + gameObject.name);

        RaycastHit hit;
        Debug.DrawRay(it.Player.position, it.Player.forward);
        if (Physics.Raycast(it.Player.position, it.Player.forward, out hit, 1f))
        {
            Debug.Log(hit.collider.gameObject);
            if (hit.collider.GetComponent<IAMovement>() != null)
            {
                hit.collider.GetComponent<IAMovement>().Hited(transform.position, true);
            }
        }
    }

}
