using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    public string NameOfScene;

    public void go()
    {
        SceneManager.LoadScene(NameOfScene);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
