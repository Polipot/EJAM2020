using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IAManager : MonoBehaviour
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

    void Start()
    {
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
                }
                theMovement.myType = Type.Civilian;
            }

            if (Portraits.Length > 0)
            {
                for (int x = 0; x < Portraits.Length; x++)
                {
                    if (x < Targets_.Count)
                    {
                        Portraits[x].SetActive(true);
                    }
                }
            }


            theMovement.Activation();
            Population.Add(theMovement);

            a += 1;
        }
    }

    private void LateUpdate()
    {
        if(NeedsToUpdate)
        {
            NeedsToUpdate = true;

            List<IAMovement> Survivants = new List<IAMovement>();

            for (int i = 0; i < Population.Count; i++)
            {
                if(Population[i].myAction != Action.Dead)
                {
                    Survivants.Add(Population[i]);
                }
                else
                {
                    Destroy(Population[i].gameObject);
                }
            }

            Population = Survivants;
        }
    }

    private void FixedUpdate()
    {
        TargetCount_t.text = Targets_.Count.ToString();
    }
}
