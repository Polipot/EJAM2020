using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_life : Singleton<Player_life>
{
    public Transform CarHere = null;
    public Transform PointToCar;

    private void Update()
    {
        if (CarHere != null)
        {
            PointToCar.gameObject.SetActive(true);
            PointToCar.LookAt(CarHere);
        }
    }

    public void Hited()
    {
        Debug.Log("PLAYER IS DEAD ");
    }

    public void GetCar(Transform car)
    {
        CarHere = car;
    }
}
