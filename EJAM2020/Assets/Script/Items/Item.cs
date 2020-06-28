using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public bool OnPlayer = false;
    public Transform Player;

    public void DropOrGet(bool val, Transform player)
    {
        OnPlayer = val;
        Player = player;
    }

    public void EnableCollider(bool val)
    {
        if (GetComponent<Player_knife>() != null)
        {
            GetComponent<Player_knife>().bc.enabled = val;
            GetComponent<Player_knife>().bcItem.enabled = false;
        }
    }
}
