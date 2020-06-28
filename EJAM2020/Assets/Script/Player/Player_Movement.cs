using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PlayerAction { Idle, Run }

public class Player_Movement : Singleton<Player_Movement>
{
    Text Dialogue_text;
    GameObject temp;

    [HideInInspector]
    public string SkinChemin;
    public Animator myAnimator;
    Rigidbody rb;
    public float Speed;

    [HideInInspector]
    public PlayerAction myPlayerAction;

    void Awake()
    {
        if (Instance != this)
            Destroy(gameObject);

        rb = GetComponent<Rigidbody>();
        myAnimator = GetComponentInChildren<Animator>();

        GetComponentInChildren<aSkin>().LoadSkin(this);
    }

    void Update()
    {
        Move();
        LookMouse();
        Interaction();
    }

    void Interaction()
    {
        if (Input.GetKeyDown(KeyCode.E) && temp != null && temp.GetComponent<Piege>().Amorçé == false)
        {
            Dialogue_text.text = "";
            temp.GetComponent<Piege>().Amorçé = true;
            temp = null;
        }
    }

    private void Move()
    {
        rb.velocity = new Vector3(Input.GetAxis("Horizontal") * Speed, 0, Input.GetAxis("Vertical") * Speed);

        if (myPlayerAction == PlayerAction.Idle && rb.velocity.magnitude > 0)
        {
            myPlayerAction = PlayerAction.Run;
            myAnimator.SetTrigger("Run");
            myAnimator.SetBool("Moving", true);
        }
        else if (myPlayerAction == PlayerAction.Run && rb.velocity.magnitude == 0)
        {
            myPlayerAction = PlayerAction.Idle;
            myAnimator.SetTrigger("StopMovement");
            myAnimator.SetBool("Moving", false);
        }    
    }

    void LookMouse()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.y = 0;
        transform.LookAt(mousePos);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Piege") && other.GetComponent<Piege>().Amorçé == false)
        {
            if(Dialogue_text == null)
            {
                Dialogue_text = GetComponent<Player_Inventory>().Dialogue_text;
            }

            GetInteractibles(other.gameObject);

            Dialogue_text.text = "Press E to place the trap";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Piege"))
        {
            Dialogue_text.text = "";
            GetInteractibles(null);
        }
    }

    void GetInteractibles(GameObject tp)
    {
        temp = tp;
    }
}
