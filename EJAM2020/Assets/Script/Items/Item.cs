using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public bool OnPlayer = false;

    public void DropOrGet(bool val)
    {
        OnPlayer = val;
    }
}
