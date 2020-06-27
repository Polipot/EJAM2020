using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum Action { None, Move, Flee, Attack, Dance }
public enum Type { Civilian, Guard, Policeman }

public class IAMovement : MonoBehaviour
{
    [Header("Piece")]
    public aRoom actualRoom;

    float MoveTime;
    float MoveLatence;

    RoomManager RM;

    NavMeshAgent myNavMesh;

    public Action myAction;
    public Type myType;

    // Start is called before the first frame update
    void Awake()
    {
        myNavMesh = GetComponent<NavMeshAgent>();
        RM = RoomManager.Instance;
    }

    private void Start()
    {
        RandomChangeRoom();

        MoveLatence = Random.Range(7f, 20f);
    }

    // Update is called once per frame
    void Update()
    {
        if(myAction == Action.None)
        {
            MoveTime += Time.deltaTime;
            if(MoveTime >= MoveLatence)
            {
                MoveTime = 0;
                RandomChangeRoom();
            }
        }

        else if(myAction == Action.Move)
        {
            if(myNavMesh.desiredVelocity.magnitude == 0)
            {
                myAction = Action.None;
            }
        }
    }

    void RandomChangeRoom()
    {
        aRoom theRoom = null;

        if(actualRoom != null && (Random.Range(0, 4) > 0))
        {
            theRoom = actualRoom;
        }
        else
        {
            aRoom theNewRoom = RM.Rooms[Random.Range(0, RM.Rooms.Count)];

            if (Random.Range(0, 2) <= (int)theNewRoom.Densité)
            {
                theRoom = theNewRoom;
            }
        }

        if(theRoom == null)
        {
            RandomChangeRoom();
        }
        else
        {
            actualRoom = theRoom;
            myAction = Action.Move;
            myNavMesh.SetDestination(new Vector3(Random.Range(theRoom.downLeft.x, theRoom.downRight.x), 0, Random.Range(theRoom.downLeft.z, theRoom.upLeft.z)));

            if (!theRoom.Population.ContainsKey(this))
            {
                theRoom.Population.Add(this, gameObject.name);
            }
        }
    }
}
