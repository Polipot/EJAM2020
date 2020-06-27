using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLike : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<IAMovement>() != null && collision.gameObject.GetComponent<IAMovement>().myAction != Action.Paralysed)
        {
            collision.gameObject.GetComponent<IAMovement>().Hited(transform.position);
        }
    }
}
