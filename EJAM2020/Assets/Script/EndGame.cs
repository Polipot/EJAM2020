using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    bool isFinished = false;

    public Collider col;
    public GameObject loser;
    public GameObject tuto;

    void changeScene()
    {
        SceneManager.LoadScene("Menu");
    }

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
        if (other.CompareTag("Player") && isFinished)
        {
            tuto.SetActive(false);
            loser.SetActive(true);
            Invoke("changeScene", 3f);
        }
    }

}
