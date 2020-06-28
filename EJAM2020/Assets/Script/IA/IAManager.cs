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
    public GameObject thePortraitPanel;

    [Header("Surveillance")]
    public string KnownPath;
    public bool theRedAlert;
    public Dictionary<IAMovement, string> Poursuivants;

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
                    theMovement.mySkin = theMovement.transform.GetComponentInChildren<aSkin>();
                    theMovement.mySkin.LoadSkin(theMovement);
                    Targets_.Add(theMovement);
                    Limite_target--;
                    //Debug.Log("IM A TARGET");

                    var p = (GameObject)Instantiate(Resources.Load("Particles/HERE"), anAI.transform);
                    p.transform.position = anAI.transform.position;

                }
                theMovement.myType = Type.Civilian;
            }

            theMovement.Activation();
            Population.Add(theMovement);

            a += 1;

            Poursuivants = new Dictionary<IAMovement, string>();
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

    public void AddPortraits(string path)
    {
        GameObject newPortrait = Instantiate(Resources.Load<GameObject>("Prefabs/aPortrait"), thePortraitPanel.transform);
        newPortrait.GetComponent<Image>().sprite = Resources.Load<Sprite>(path + "/Tête");
    }

    public void DeletePortrait(Sprite theTête)
    {
        for (int i = 0; i < thePortraitPanel.transform.childCount; i++)
        {
            if(thePortraitPanel.transform.GetChild(i).GetComponent<Image>().sprite == theTête)
            {
                thePortraitPanel.transform.GetChild(i).gameObject.SetActive(false);
                break;
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
                Population[i].Radar.GetComponent<SpriteRenderer>().color = new Color(Color.red.r, Color.red.g, Color.red.b , Population[i].Radar.GetComponent<SpriteRenderer>().color.a);
                Population[i].PortéeSurveillance += 2;
            }
        }
    }

    public void EndAlert()
    {
        theRedAlert = false;

        for (int i = 0; i < Population.Count; i++)
        {
            if (Population[i].myType != Type.Civilian)
            {
                Population[i].Radar.GetComponent<SpriteRenderer>().color = new Color(Color.yellow.r, Color.yellow.g, Color.yellow.b, Population[i].Radar.GetComponent<SpriteRenderer>().color.a);
                Population[i].PortéeSurveillance -= 2;
            }
        }
    }
}
