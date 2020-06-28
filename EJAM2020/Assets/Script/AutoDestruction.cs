using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestruction : MonoBehaviour
{
    public float Latence;
    float Temps;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Temps += Time.deltaTime;
        if(Temps >= Latence)
        {
            Destroy(gameObject);
        }
    }
}
