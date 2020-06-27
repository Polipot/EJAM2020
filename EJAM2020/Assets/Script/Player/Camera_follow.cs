using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_follow : Singleton<Camera_follow>
{
    [Range(0, 20)]
    public float smothness;
    public Transform target;

    private void Awake()
    {
        if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

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
