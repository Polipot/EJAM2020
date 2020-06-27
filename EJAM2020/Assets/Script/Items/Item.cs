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
}
