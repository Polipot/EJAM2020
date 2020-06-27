using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PopValeur { Faible, Moyenne, Forte }

public class aRoom : MonoBehaviour
{
    RoomManager RM;

    [Header("Stats")]
    public PopValeur Densité;
    [HideInInspector]
    public Dictionary<IAMovement, string> Population;

    [HideInInspector]
    public Vector3 upRight, upLeft, downRight, downLeft;

    // Start is called before the first frame update
    void Awake()
    {
        BoxCollider myBoxCollider = GetComponent<BoxCollider>();

        upRight = transform.TransformPoint(myBoxCollider.center + new Vector3(myBoxCollider.size.x, 0 ,myBoxCollider.size.z) * 0.5f);
        upLeft = transform.TransformPoint(myBoxCollider.center + new Vector3(-myBoxCollider.size.x, 0, myBoxCollider.size.z) * 0.5f);
        downRight = transform.TransformPoint(myBoxCollider.center + new Vector3(myBoxCollider.size.x, 0, -myBoxCollider.size.z) * 0.5f);
        downLeft = transform.TransformPoint(myBoxCollider.center + new Vector3(-myBoxCollider.size.x, 0, -myBoxCollider.size.z) * 0.5f);

        Population = new Dictionary<IAMovement, string>();

        RM = RoomManager.Instance;
        RM.Rooms.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(upRight, upLeft);
        Gizmos.DrawLine(upLeft, downLeft);
        Gizmos.DrawLine(downLeft, downRight);
        Gizmos.DrawLine(downRight, upRight);
    }
}
