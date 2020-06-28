using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aSpawnPoint : MonoBehaviour
{
    IAManager IAM;

    // Start is called before the first frame update
    void Awake()
    {
        IAManager.Instance.SpawnPoints.Add(transform);
        IAM = IAManager.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Oui");
        if(other.GetComponent<IAMovement>() != null && other.GetComponent<IAMovement>().myAction == Action.Leave)
        {
            IAM.SomeoneLeave(other.GetComponent<IAMovement>());
        }
    }
}
