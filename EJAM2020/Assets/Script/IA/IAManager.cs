using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAManager : MonoBehaviour
{
    [HideInInspector]
    public bool NeedsToUpdate;
    [HideInInspector]
    public List<IAMovement> Population;

    [Header("Temps")]
    [HideInInspector]
    public float TempsNouveauxArrivants;
    public int LimitePopulation;
    public int PersonnesPourUnGarde;
    public float LatenceNouveauxArrivants;

    // Update is called once per frame
    void Start()
    {
        int a = 0;

        for (int i = 0; i < LimitePopulation; i++)
        {
            GameObject anAI = (GameObject)Instantiate(Resources.Load<GameObject>("Prefabs/IA"), Vector3.zero, transform.rotation);
            IAMovement theMovement = anAI.GetComponent<IAMovement>();
            if (a >= PersonnesPourUnGarde)
            {
                theMovement.myType = Type.Guard;                
                a = 0;
            }
            else
            {
                theMovement.myType = Type.Civilian;
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
}
