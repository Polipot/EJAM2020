using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum Action { None, Move, Flee, Attack, Dance, Dead, Paralysed }
public enum Type { Civilian, Guard, Policeman }

public class IAMovement : MonoBehaviour
{
    public bool Die;

    [Header("Piece")]
    public aRoom actualRoom;

    [Header("Temps")]
    float MoveTime;
    float MoveLatence;

    [Header("Surveillance")]
    public int PortéeSurveillance;

    RoomManager RM;

    NavMeshAgent myNavMesh;
    Animator myAnimator;

    public Action myAction;
    public Type myType;

    // Start is called before the first frame update
    void Awake()
    {
        myNavMesh = GetComponent<NavMeshAgent>();
        myAnimator = GetComponentInChildren<Animator>();
        RM = RoomManager.Instance;
    }

    private void Start()
    {
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
        transform.GetChild(1).localScale = new Vector3(PortéeSurveillance, PortéeSurveillance, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if(myAction != Action.Dead)
        {
            if (myAction == Action.None || myAction == Action.Dance)
            {
                MoveTime += Time.deltaTime;
                if (MoveTime >= MoveLatence)
                {
                    MoveTime = 0;
                    RandomChangeRoom();
                }
            }

            else if(myAction == Action.Paralysed)
            {
                MoveTime += Time.deltaTime;
                if(MoveTime >= 1)
                {
                    MoveTime = 0;
                    Fuite();
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

    void Hited(Vector3 HitingEntity, bool Lethal = false)
    {
        myNavMesh.enabled = false;
        Vector3 Direction = HitingEntity - transform.position;
        MoveTime = 0;
        myAction = Action.Paralysed;
        myAnimator.SetTrigger("Hited");

        List<IAMovement> theEnnemies = new List<IAMovement>();

        foreach (IAMovement item in actualRoom.Population.Keys)
        {
            theEnnemies.Add(item);
        }

        for (int i = 0; i < theEnnemies.Count; i++)
        {
            if (theEnnemies[i].myType == Type.Civilian)
            {
                theEnnemies[i].Fuite();
            }
            else
            {
                // Poursuite;
            }
        }

        if (Lethal)
        {
            Mort();
        }
    }

    void Mort()
    {
        myAction = Action.Dead;
    }
}
