using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerAction { Idle, Run }

public class Player_Movement : Singleton<Player_Movement>
{
    Animator myAnimator;
    Rigidbody rb;
    public float Speed;

    void Awake()
    {
        if (Instance != this)
            Destroy(gameObject);

        rb = GetComponent<Rigidbody>();
        myAnimator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        Move();
        LookMouse();
    }


    private void Move()
    {
        if(rb.velocity.magnitude == 0 && (Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Vertical") > 0))
        {
            myAnimator.SetTrigger("Run");
        }
        
        rb.velocity = new Vector3(Input.GetAxis("Horizontal")* Speed, 0, Input.GetAxis("Vertical") * Speed);
        
        if (rb.velocity.magnitude != 0 && (Input.GetAxis("Horizontal") != 1 && Input.GetAxis("Vertical") != 1))
        {
            myAnimator.SetTrigger("Idle");
        }
    }

    void LookMouse()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.y = 0;
        transform.LookAt(mousePos);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z);
    }
}
