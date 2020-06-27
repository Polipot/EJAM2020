using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IAManager : Singleton<IAManager>
{
    [HideInInspector]
    public bool NeedsToUpdate;
    [HideInInspector]
    public List<IAMovement> Population;

    [Header("Temps")]
    [HideInInspector]
    public float TempsNouveauxArrivants;
    [Range(10, 150)]
    public int LimitePopulation;
    public int PersonnesPourUnGarde;
    public float LatenceNouveauxArrivants;

    [Header("Targets")]
    [Range(0, 4)]
    public int Limite_target;
    public List<IAMovement> Targets_;
    public Text TargetCount_t;
    public GameObject[] Portraits;

    [Header("Surveillance")]
    public string KnownPath;
    public bool theRedAlert;

    void Awake()
    {
        if (Instance != this)
            Destroy(gameObject);


        int a = 0;

        for (int i = 0; i < LimitePopulation; i++)
        {
            GameObject anAI = Instantiate(Resources.Load<GameObject>("Prefabs/IA"),
                Vector3.zero, transform.rotation);

            IAMovement theMovement = anAI.GetComponent<IAMovement>();
            if (a >= PersonnesPourUnGarde)
            {
                theMovement.myType = Type.Guard;                
                a = 0;
            }
            else
            {
                if (i % 2 == 1 && Limite_target > 0)
                {
                    theMovement.IsTarget = true;
                    Targets_.Add(theMovement);
                    Limite_target--;
                    //Debug.Log("IM A TARGET");

                    var p = (GameObject)Instantiate(Resources.Load("Particles/HERE"), anAI.transform);
                    p.transform.position = anAI.transform.position;

                }
                theMovement.myType = Type.Civilian;
            }

            RefreshPortraits();

            theMovement.Activation();
            Population.Add(theMovement);

            a += 1;
        }
    }

    private void LateUpdate()
    {
        if(NeedsToUpdate)
        {
            NeedsToUpdate = false;

            List<IAMovement> Survivants = new List<IAMovement>();

            for (int i = 0; i < Population.Count; i++)
            {
                if(Population[i].myAction != Action.Dead)
                {
                    Survivants.Add(Population[i]);
                }
                else
                {
                    Targets_.Remove(Population[i]);
                    Destroy(Population[i].gameObject);

                    RefreshPortraits();
                }
            }

            Population = Survivants;
        }
    }

    private void FixedUpdate()
    {
        if (TargetCount_t != null)
        {
            TargetCount_t.text = Targets_.Count.ToString();
        }
    }

    void RefreshPortraits()
    {
        if (Portraits.Length > 0)
        {
            for (int x = 0; x < Portraits.Length; x++)
            {
                Portraits[x].SetActive(false);

                if (x < Targets_.Count)
                {
                    Portraits[x].SetActive(true);
                }
            }
        }
    }

    public void RedAlert()
    {
        theRedAlert = true;

        for (int i = 0; i < Population.Count; i++)
        {
            if(Population[i].myType != Type.Civilian)
            {
                Population[i].PortéeSurveillance += 2;
            }
        }
    }
}
