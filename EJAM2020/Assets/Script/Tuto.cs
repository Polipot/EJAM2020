using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tuto : Singleton<Tuto>
{
    bool isActive = true;

    int step;
    public GameObject Menu;
    public GameObject canvasTuto;
    public GameObject bt;
    public GameObject BtHelp;
    public GameObject[] Blocs;

    public string inno, garde, police;
    public GameObject Pepole_g;
    public Text Pepole;

    bool first = false;

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isActive = !isActive;
            Menu.SetActive(isActive);
            if (isActive)
            {
                Time.timeScale = 0;
            }
            else Time.timeScale = 1;
        }

        switch (step)
        {
            case 0:
                Time.timeScale = 0;
                for (int i = 0; i < Blocs.Length; i++)
                {
                    Blocs[i].SetActive(false);
                }
                Blocs[0].SetActive(true);
                break;

            case 1:
                Time.timeScale = 0;
                for (int i = 0; i < Blocs.Length; i++)
                {
                    Blocs[i].SetActive(false);
                }
                Blocs[1].SetActive(true);
                break;

            case 2:
                Time.timeScale = 0;
                for (int i = 0; i < Blocs.Length; i++)
                {
                    Blocs[i].SetActive(false);
                }
                Blocs[2].SetActive(true);
                break;

            case 3:
                for (int i = 0; i < Blocs.Length; i++)
                {
                    Blocs[i].SetActive(false);
                }
                bt.SetActive(false);
                Time.timeScale = 1;
                step++;
                break;

            case 5:
                bt.SetActive(true);
                BtHelp.SetActive(false);
                Pepole_g.SetActive(true);
                Pepole.text = inno;
                Time.timeScale = 0;
                break;

            case 6:
                BtHelp.SetActive(false);
                Pepole_g.SetActive(true);
                Pepole.text = garde;
                Time.timeScale = 0;
                break;

            case 7:
                BtHelp.SetActive(false);
                Pepole_g.SetActive(true);
                Pepole.text = police;
                Time.timeScale = 0;
                break;

            default:
                break;
        }
    }

    private void Awake()
    {
        if (Instance != this)
            Destroy(gameObject);

        Pepole_g.SetActive(false);
        Menu.SetActive(false);

        first = false;

        if (isActive)
        {
            Time.timeScale = 0;
        }
    }

    public void NextBt()
    {
        step++;
        if (step > 7)
        {
            Pepole_g.SetActive(false);
            Pepole.text = "";
            bt.SetActive(false);
            Time.timeScale = 1;
            BtHelp.SetActive(true);
        }
    }

    public void helpBt()
    {
        step = 5;
    }

    public void HelpWeapon()
    {
        if (!first && canvasTuto.activeSelf)
        {

            first = true;
            Time.timeScale = 0;
            for (int i = 0; i < Blocs.Length; i++)
            {
                Blocs[i].SetActive(false);
            }
            Blocs[3].SetActive(true);
            Blocs[4].SetActive(true);
        }
    }
    public void stop()
    {
        Time.timeScale = 1;
        for (int i = 0; i < Blocs.Length; i++)
        {
            Blocs[i].SetActive(false);
        }
    }

    public void ActiveTuto(bool val)
    {
        canvasTuto.SetActive(val);
    }

    public void Play()
    {
        isActive = !isActive;
        Menu.SetActive(isActive);
        Time.timeScale = 1;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
