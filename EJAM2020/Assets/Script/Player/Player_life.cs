using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_life : Singleton<Player_life>
{
    public Transform CarHere = null;
    public Transform PointToCar;

    public GameObject loser;
    public GameObject tuto;

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
        tuto.SetActive(false);
        loser.SetActive(true);
        Invoke("changeScene", 3f);
    }

    void changeScene()
    {
        SceneManager.LoadScene("Menu");
    }

    public void GetCar(Transform car)
    {
        CarHere = car;
    }
}
