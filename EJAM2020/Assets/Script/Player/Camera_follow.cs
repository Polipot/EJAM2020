using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_follow : MonoBehaviour
{
    [Range(0, 20)]
    public float smothness;
    public Transform target;

    void FixedUpdate()
    {
        SmoothFollow();
    }

    void SmoothFollow()
    {
        if (target != null)
        {
            Vector3 smooth = target.position - transform.position;
            transform.position += smooth / smothness;
        }
    }
}
