using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tuto : MonoBehaviour
{
    bool isActive = true;

    int step;
    public GameObject canvasTuto;
    public GameObject bt;
    public GameObject[] Blocs;

    public string inno, garde, police;
    public GameObject Pepole_g;
    public Text Pepole;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isActive = !isActive;
            canvasTuto.SetActive(isActive);
        }

        switch (step)
        {
            case 0:
                for (int i = 0; i < Blocs.Length; i++)
                {
                    Blocs[i].SetActive(false);
                }
                Blocs[0].SetActive(true);
                break;

            case 1:
                for (int i = 0; i < Blocs.Length; i++)
                {
                    Blocs[i].SetActive(false);
                }
                Blocs[1].SetActive(true);
                break;

            case 2:
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
                break;

            default:
                break;
        }
    }

    private void Awake()
    {
        Pepole_g.SetActive(false);

        if (isActive)
        {
            Time.timeScale = 0;
        }
    }

    public void NextBt()
    {
        step++;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<IAMovement>() != null)
        {
            IAMovement ia = other.GetComponent<IAMovement>();

            if (ia.myType == Type.Civilian)
            {
                Pepole_g.SetActive(true);
                Pepole.text = inno;
            }

            if (ia.myType == Type.Guard)
            {
                Pepole_g.SetActive(true);
                Pepole.text = garde;
            }

            if (ia.myType == Type.Policeman)
            {
                Pepole_g.SetActive(true);
                Pepole.text = police;
            }
        }
    }
}
