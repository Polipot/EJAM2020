﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aSpawnPoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        IAManager.Instance.SpawnPoints.Add(transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
