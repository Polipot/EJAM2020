using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum Action { None, Move, Flee, Attack, Dance }
public enum Type { Civilian, Guard, Policeman }

public class IAMovement : MonoBehaviour
{
    NavMeshAgent myNavMesh;

    public Action myAction;
    public Type myType;

    // Start is called before the first frame update
    void Awake()
    {
        myNavMesh = GetComponent<NavMeshAgent>();
        
    }

    private void Start()
    {
        ChangeRoom();
    }

    // Update is called once per frame
    void Update()
    {
        // Pour les tests
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if(Physics.Raycast(mousePosition, Vector3.down, out RaycastHit Hit, 10))
            {
                myNavMesh.SetDestination(Hit.point);
            }
        }
    }

    void ChangeRoom()
    {
        aRoom theRoom = GameObject.Find("aRoom").GetComponent<aRoom>();
        myNavMesh.SetDestination(new Vector3(Random.Range(theRoom.downLeft.x, theRoom.downRight.x), 0, Random.Range(theRoom.downLeft.z, theRoom.upLeft.z)));

        if (!theRoom.Population.ContainsKey(this))
        {
            theRoom.Population.Add(this, gameObject.name);
        }
    }
}
