using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourtCircuit : MonoBehaviour
{
    [Header("Attributs")]
    public bool Actif;
    public float Latence;
    float Temps;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Actif)
        {
            Temps += Time.deltaTime;
            if(Temps >= Latence)
            {
                Actif = false;
            }
        }
    }

    public void Activation()
    {
        Actif = true;
    }
}
