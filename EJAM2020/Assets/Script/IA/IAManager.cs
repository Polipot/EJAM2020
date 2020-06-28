using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IAManager : Singleton<IAManager>
{
    [HideInInspector]
    public bool NeedsToUpdate;
    public bool SomeoneLeft;
    [HideInInspector]
    public List<IAMovement> Population;

    [Header("Temps")]
    public bool needNewPopulation;
    [HideInInspector]
    public float TempsNouveauxArrivants;
    [Range(10, 150)]
    public int LimitePopulation;
    public int PersonnesPourUnGarde;
    [Range(10, 150)]
    public float LatenceNouveauxArrivants;

    [Header("Targets")]
    [Range(0, 4)]
    public int Limite_target;
    public List<IAMovement> Targets_;
    public Text TargetCount_t;
    public GameObject[] Portraits;
    public GameObject thePortraitPanel;

    [Header("Surveillance")]
    public bool PolicemanOnGround;
    public string KnownPath;
    public bool theRedAlert;
    public Dictionary<IAMovement, string> Poursuivants;
    [HideInInspector]
    public float TempsPolice;
    [HideInInspector]
    public int VictimesActuelles;
    [Range(1, 10)]
    public int VictimesPourPolice;
    public float LatencePolice;
    bool PoliceIncoming;

    [Header("Spawn"),HideInInspector]
    public List<Transform> SpawnPoints;

    void Awake()
    {
        if (Instance != this)
            Destroy(gameObject);


        int a = 0;

        for (int i = 0; i < LimitePopulation; i++)
        {
            int theIndexSpawn = Random.Range(0, SpawnPoints.Count);

            GameObject anAI = Instantiate(Resources.Load<GameObject>("Prefabs/IA"),
                SpawnPoints[theIndexSpawn].position, transform.rotation);

            IAMovement theMovement = anAI.GetComponent<IAMovement>();

            bool mustBeATarget = false;
            if (a >= PersonnesPourUnGarde)
            {
                theMovement.myType = Type.Guard;                
                a = 0;
            }
            else
            {
                if (i % 2 == 1 && Limite_target > 0)
                {
                    Targets_.Add(theMovement);
                    Limite_target--;
                    //Debug.Log("IM A TARGET");
                    mustBeATarget = true;
                    var p = (GameObject)Instantiate(Resources.Load("Particles/HERE"), anAI.transform);
                    p.transform.position = anAI.transform.position;

                }
                theMovement.myType = Type.Civilian;
            }

            theMovement.Activation(mustBeATarget, true);
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

                    needNewPopulation = true;

                    if(PoliceIncoming == false && PolicemanOnGround == false)
                    {
                        VictimesActuelles += 1;
                        if(VictimesActuelles >= VictimesPourPolice)
                        {
                            PoliceIncoming = true;
                        }
                    }
                }
            }

            Population = Survivants;
        }

        if (SomeoneLeft)
        {
            List<IAMovement> CeuxQuiRestent = new List<IAMovement>();

            for (int i = 0; i < Population.Count; i++)
            {
                if (!Population[i].ReadytoLeave)
                {
                    CeuxQuiRestent.Add(Population[i]);
                }
                else
                {
                    Destroy(Population[i].gameObject);
                }
            }

            Population = CeuxQuiRestent;
        }
    }

    private void FixedUpdate()
    {
        if (TargetCount_t != null)
        {
            TargetCount_t.text = Targets_.Count.ToString();
        }
    }

    private void Update()
    {
        if (PoliceIncoming)
        {
            TempsPolice += Time.deltaTime;
            if(TempsPolice >= LatencePolice)
            {
                SpawnPoliceman();
                PolicemanOnGround = true;
                VictimesActuelles = 0;
                PoliceIncoming = false;
                TempsPolice = 0;
            }
        }

        if (needNewPopulation)
        {
            TempsNouveauxArrivants += Time.deltaTime;
            if(TempsNouveauxArrivants >= LatenceNouveauxArrivants)
            {
                needNewPopulation = false;
                TempsNouveauxArrivants = 0;
                int nombreNecessaire = LimitePopulation - Population.Count;

                for (int i = 0; i < nombreNecessaire; i++)
                {
                    int theIndexSpawn = Random.Range(0, SpawnPoints.Count);

                    GameObject anAI = Instantiate(Resources.Load<GameObject>("Prefabs/IA"),
                SpawnPoints[theIndexSpawn].position, transform.rotation);

                    IAMovement theMovement = anAI.GetComponent<IAMovement>();

                    theMovement.myType = Type.Civilian;
                    theMovement.Activation();
                    Population.Add(theMovement);
                }
            }
        }
    }

    public void AddPortraits(string path)
    {
        GameObject newPortrait = Instantiate(Resources.Load<GameObject>("Prefabs/aPortrait"), thePortraitPanel.transform);
        newPortrait.GetComponent<Image>().sprite = Resources.Load<Sprite>(path + "/Tête");
    }

    public void DeletePortrait(string path)
    {
        Sprite theTête = Resources.Load<Sprite>(path + "/Tête");

        for (int i = 0; i < thePortraitPanel.transform.childCount; i++)
        {
            if(thePortraitPanel.transform.GetChild(i).gameObject.activeSelf == true && thePortraitPanel.transform.GetChild(i).GetComponent<Image>().sprite == theTête)
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

    public void SpawnPoliceman()
    {
        int theIndexSpawn = Random.Range(0, SpawnPoints.Count);

        GameObject anAI = Instantiate(Resources.Load<GameObject>("Prefabs/IA"),
    SpawnPoints[theIndexSpawn].position, transform.rotation);

        IAMovement theMovement = anAI.GetComponent<IAMovement>();

        theMovement.myType = Type.Policeman;
        theMovement.Activation();
        Population.Add(theMovement);
    }

    public void SpawnCivil()
    {
        int theIndexSpawn = Random.Range(0, SpawnPoints.Count);

        GameObject anAI = Instantiate(Resources.Load<GameObject>("Prefabs/IA"),
    SpawnPoints[theIndexSpawn].position, transform.rotation);

        IAMovement theMovement = anAI.GetComponent<IAMovement>();

        theMovement.myType = Type.Civilian;
        theMovement.Activation();
        Population.Add(theMovement);
    }

    public void SomeoneLeave(IAMovement theIA)
    {
        theIA.ReadytoLeave = true;
        SomeoneLeft = true;
    }
}
