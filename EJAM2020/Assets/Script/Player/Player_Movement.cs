using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : Singleton<Player_Movement>
{
    Rigidbody rb;
    public float Speed;

    void Awake()
    {
        if (Instance != this)
        {
            Destroy(gameObject);
        }

        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Move();
        LookMouse();
    }


    private void Move()
    {
        rb.velocity = new Vector3(Input.GetAxis("Horizontal")* Speed, 0, Input.GetAxis("Vertical") * Speed);
    }

    void LookMouse()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.y = 0;
        transform.LookAt(mousePos);
    }
}
