using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_life : MonoBehaviour
{
    public bool test;

    private void Update()
    {
        if (test)
        {
            Hited();
        }
    }

    public void Hited()
    {
        Debug.Log("PLAYER IS DEAD ");
        test = false;
    }
}
