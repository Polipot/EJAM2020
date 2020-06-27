using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_follow : MonoBehaviour
{
    public Transform target;

    void Update()
    {
        SmoothFollow();
    }

    void SmoothFollow()
    {
        if (target != null)
        {
            Vector3 smooth = target.position - transform.position;
            transform.position += smooth / 20;
        }
    }
}
