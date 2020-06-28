﻿using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum Action { None, Move, Flee, Attack, Dance, Dead, Paralysed, Found, Lost, Leave }
public enum Type { Civilian, Guard, Policeman }

public class IAMovement : MonoBehaviour
{
    [HideInInspector]
    public bool isBasePerso;

    IAManager IAM;
    Player_Movement PM;
    [HideInInspector]
    public aSkin mySkin;
    bool Actif;
    [HideInInspector]
    public string SkinChemin;

    public bool IsTarget;

    [Header("Piece")]
    public aRoom actualRoom;

    [Header("Temps")]
    float MoveTime;
    float MoveLatence;

    [Header("Surveillance")]
    public int PortéeSurveillance;
    public GameObject Radar;
    public LayerMask PoursuiteLayer;

    [Header("Police")]
    public int indexRoom;
    float TempsRestant = 10;
    [HideInInspector]
    public bool ReadytoLeave;

    RoomManager RM;

    NavMeshAgent myNavMesh;
    Animator myAnimator;

    public Action myAction;
    public Type myType;

    public void Activation(bool isATarget = false, bool isActivation = false)
    {
        myNavMesh = GetComponent<NavMeshAgent>();
        myAnimator = GetComponentInChildren<Animator>();
        RM = RoomManager.Instance;
        PM = Player_Movement.Instance;
        IAM = IAManager.Instance;

        RandomChangeRoom();

        MoveLatence = Random.Range(7f, 20f);

        switch (myType)
        {
            case Type.Civilian:
                PortéeSurveillance = 0;
                break;
            case Type.Guard:
                PortéeSurveillance = 6;
                break;
            case Type.Policeman:
                PortéeSurveillance = 10;
                break;
            default:
                break;
        }
        Radar = transform.GetChild(1).gameObject;
        Radar.transform.localScale = new Vector3(PortéeSurveillance, PortéeSurveillance, 1);
        Radar.GetComponent<SpriteRenderer>().color = new Color(Color.yellow.r, Color.yellow.g, Color.yellow.b, Radar.GetComponent<SpriteRenderer>().color.a);
        if (isATarget)
        {
            IsTarget = true;
        }
        mySkin = GetComponentInChildren<aSkin>();
        mySkin.LoadSkin(this);
        if (isActivation)
        {
            isBasePerso = true;
        }
        Actif = true;

        
        if (IAM.theRedAlert)
        {
            Radar.GetComponent<SpriteRenderer>().color = new Color(Color.red.r, Color.red.g, Color.red.b, Radar.GetComponent<SpriteRenderer>().color.a);
            PortéeSurveillance += 2;
        }
        UpdateRadar();
    }

    // Update is called once per frame
    void Update()
    {
        if (Actif)
        {
            if (myAction != Action.Dead)
            {
                if (myAction == Action.None || myAction == Action.Dance)
                {
                    MoveTime += Time.deltaTime;
                    if (MoveTime >= MoveLatence)
                    {
                        MoveTime = 0;
                        myNavMesh.speed = 1.5f;
                        RandomChangeRoom();
                    }
                }

                else if (myAction == Action.Paralysed)
                {
                    MoveTime += Time.deltaTime;
                    if (MoveTime >= 1)
                    {
                        MoveTime = 0;
                        myNavMesh.enabled = true;
                        Fuite();
                    }
                }

                else if (myAction == Action.Lost)
                {
                    MoveTime += Time.deltaTime;
                    if (MoveTime >= 2)
                    {
                        MoveTime = 0;
                        myNavMesh.enabled = true;
                        myNavMesh.speed = 1.5f;
                        if(myType == Type.Policeman && TempsRestant <= 0)
                        {
                            PoliceStartLeave();
                        }
                        else
                        {
                            RandomChangeRoom(false);
                        }
                    }
                }

                else if (myAction == Action.Found)
                {
                    MoveTime += Time.deltaTime;
                    transform.LookAt(new Vector3(PM.gameObject.transform.position.x, transform.position.y, PM.gameObject.transform.position.z));
                    if (MoveTime >= 1)
                    {
                        MoveTime = 0;
                        myNavMesh.enabled = true;
                        StartPursuit();
                    }
                }

                else if (myAction == Action.Attack)
                {
                    myNavMesh.SetDestination(PM.gameObject.transform.position);
                    Vector3 Direction = (PM.transform.position - transform.position).normalized;
                    RaycastHit[] ToPlayer = Physics.RaycastAll(transform.position, Direction, 100, PoursuiteLayer).OrderBy(h => h.distance).ToArray(); ;

                    for (int i = 0; i < ToPlayer.Length; i++)
                    {
                        if (ToPlayer[i].collider.tag.Equals("Player"))
                        {
                            // a toujours le joueur en vue
                            break;
                        }
                        else if (ToPlayer[i].collider.tag.Equals("Wall"))
                        {
                            // a perdu la trace du joueur
                            LostInPursuit();
                            break;
                        }
                    }
                }

                else if (myAction == Action.Move || myAction == Action.Flee)
                {
                    if (myNavMesh.desiredVelocity.magnitude == 0 && Vector3.Distance(transform.position, myNavMesh.destination) < 2)
                    {
                        if (actualRoom.Actions_Spéciales.Count == 0 || myType != Type.Civilian)
                        {
                            myAction = Action.None;
                            myAnimator.SetTrigger("StopMovement");
                        }
                        else
                        {
                            myAction = actualRoom.Actions_Spéciales[0];
                            myAnimator.SetTrigger("Dance");
                        }
                    }
                }

                if( myType != Type.Civilian)
                {
                    UpdateRadar();

                    if(myAction != Action.Attack && myAction != Action.Found)
                    {
                        Watchout();
                        if(myType == Type.Policeman && myAction != Action.Leave && myAction != Action.Lost)
                        {
                            TempsRestant -= Time.deltaTime;
                            if(TempsRestant <= 0)
                            {
                                PoliceStartLeave();
                            }
                        }
                    }
                }
            }
        }
    }

    void RandomChangeRoom(bool isFuite = false)
    {
        aRoom theRoom = null;

        if(actualRoom != null && (Random.Range(0, 4) > 0) && isFuite == false)
        {
            theRoom = actualRoom;
        }
        else
        {
            aRoom theNewRoom = RM.Rooms[Random.Range(0, RM.Rooms.Count)];

            if (Random.Range(0, 2) <= (int)theNewRoom.Densité && theNewRoom != actualRoom)
            {
                theRoom = theNewRoom;
            }
        }

        if(theRoom == null)
        {
            RandomChangeRoom(isFuite);
        }
        else
        {
            myNavMesh.SetDestination(new Vector3(Random.Range(theRoom.downLeft.x, theRoom.downRight.x), 0, Random.Range(theRoom.downLeft.z, theRoom.upLeft.z)));

            if(actualRoom != theRoom)
            {
                if(actualRoom != null)
                {
                    actualRoom.Population.Remove(this);
                }

                actualRoom = theRoom;

                if (!theRoom.Population.ContainsKey(this))
                {
                    theRoom.Population.Add(this, gameObject.name);
                }
            }


            if (isFuite == false)
            {
                myAction = Action.Move;
                myAnimator.SetTrigger("Walk");
                myNavMesh.speed = 1.5f;                
            }
            else
            {
                myNavMesh.speed = 5f;
                myAnimator.SetTrigger("Run");
                myAction = Action.Flee;               
            }           
        }
    }

    void Fuite()
    {
        RandomChangeRoom(true);
    }

    public void Hited(Vector3 HitingEntity, bool Lethal = false)
    {
        myNavMesh.enabled = false;
        transform.LookAt(new Vector3(HitingEntity.x, transform.position.y, HitingEntity.z));

        MoveTime = 0;
        myAction = Action.Paralysed;
        myAnimator.SetTrigger("Hited");

        for (int i = 0; i < 3; i++)
        {
            GameObject Sang = Instantiate(Resources.Load<GameObject>("Bloods"), new Vector3(transform.position.x + Random.Range(-1f, 1f), transform.position.y, transform.position.z + Random.Range(-1f, 1f)), transform.rotation);
        }

        List<IAMovement> theEnnemies = new List<IAMovement>();

        foreach (IAMovement item in actualRoom.Population.Keys)
        {
            theEnnemies.Add(item);
        }

        bool NotSeen = true;

        for (int i = 0; i < theEnnemies.Count; i++)
        {
            if(theEnnemies[i] != this && theEnnemies[i].myAction != Action.Paralysed && Vector3.Distance(transform.position, theEnnemies[i].transform.position) <= 10)
            {
                if (theEnnemies[i].myType == Type.Civilian)
                {
                    theEnnemies[i].Fuite();
                }
                else
                {
                    NotSeen = false;
                    if (theEnnemies[i].myAction != Action.Attack && theEnnemies[i].myAction != Action.Found)
                    {
                        theEnnemies[i].Found();
                    }                                            
                }
            }
        }

        if (Lethal && myType == Type.Civilian)
        {
            if (NotSeen && IAM.Poursuivants.Count == 0)
            {
                PM.GetComponentInChildren<aSkin>().LoadSkin(PM, SkinChemin);
                if (IAM.theRedAlert)
                {
                    IAM.EndAlert();
                }                
            }
            else if (!NotSeen && !IAM.theRedAlert)
            {
                IAM.RedAlert();
            }

            Mort();
        }
    }

    void Mort()
    {
        if (IsTarget)
        {
            IAM.DeletePortrait(SkinChemin);
        }
        if (actualRoom != null)
        {
            actualRoom.Population.Remove(this);
        }

        IAManager.Instance.NeedsToUpdate = true;
        myAction = Action.Dead;
        
    }

    public void Found()
    {
        myNavMesh.enabled = false;
        MoveTime = 0;
        myAction = Action.Found;
        myAnimator.SetTrigger("Found");
    }

    public void Watchout()
    {
        Vector3 Direction = (PM.transform.position - transform.position).normalized;
        RaycastHit[] ToPlayer = Physics.RaycastAll(transform.position, Direction, PortéeSurveillance / 2, PoursuiteLayer).OrderBy(h => h.distance).ToArray();

        for (int i = 0; i < ToPlayer.Length; i++)
        {            
            if (ToPlayer[i].collider.tag.Equals("Wall"))
            {
                // a perdu la trace du joueur
                break;
            }
            else if (ToPlayer[i].collider.tag.Equals("Player"))
            {
                Found();
                // a toujours le joueur en vue
                break;
            }
        }
    }

    public void StartPursuit()
    {
        myNavMesh.enabled = true;
        myNavMesh.speed = 5f;
        myAnimator.SetTrigger("Run");
        myAction = Action.Attack;

        if (!IAM.Poursuivants.ContainsKey(this))
        {
            IAM.Poursuivants.Add(this, gameObject.name);
        }
    }

    public void LostInPursuit()
    {
        myNavMesh.enabled = false;
        MoveTime = 0;
        myAction = Action.Lost;
        myAnimator.SetTrigger("Found");

        if (IAM.Poursuivants.ContainsKey(this))
        {
            IAM.Poursuivants.Remove(this);
        }
    }

    public void PoliceStartLeave()
    {
        myAction = Action.Leave;

        int spawnIndex = Random.Range(0, IAM.SpawnPoints.Count);
        actualRoom = null;
        myNavMesh.enabled = true;
        myNavMesh.SetDestination(IAM.SpawnPoints[spawnIndex].position);

        myAnimator.SetTrigger("Walk");
        myNavMesh.speed = 1.5f;
        Debug.Log("Oui");
    }

    void UpdateRadar()
    {
        if(IAM.theRedAlert && Radar.transform.localScale.x < PortéeSurveillance)
        {
            Radar.transform.localScale = new Vector3(Radar.transform.localScale.x + 0.01f, Radar.transform.localScale.y + 0.01f, 1);
        }
        else if (!IAM.theRedAlert && Radar.transform.localScale.x > PortéeSurveillance)
        {
            Radar.transform.localScale = new Vector3(Radar.transform.localScale.x - 0.01f, Radar.transform.localScale.y - 0.01f, 1);
        }
    }
}
