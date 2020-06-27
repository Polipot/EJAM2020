using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    bool isFinished = false;

    public Collider col;

    private void FixedUpdate()
    {
        if (IAManager.Instance.Targets_.Count == 0)
        {
            isFinished = true;
        }

        if (isFinished)
        {
            col.enabled = true;
            Player_life.Instance.GetCar(transform);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("FIN DU GAME");
        }
    }
}
