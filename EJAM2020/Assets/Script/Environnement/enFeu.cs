using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enFeu : MonoBehaviour
{
    ParticleSystem theParticleSystem;
    public Transform cible;

    // Start is called before the first frame update
    void Awake()
    {
        theParticleSystem = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if(cible != null)
        {
            transform.position = cible.position;
            if (theParticleSystem.isStopped)
            {
                theParticleSystem.Play();
            }
        }
        else
        {
            if (theParticleSystem.isPlaying)
            {
                theParticleSystem.Stop();
            }
        }
    }
}
