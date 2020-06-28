using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourtCircuit : MonoBehaviour
{
    CameraShaker CS;

    ParticleSystem Decharge;

    [Header("Attributs")]
    public bool Actif;
    public float Latence;
    float Temps;

    // Start is called before the first frame update
    void Awake()
    {
        CS = CameraShaker.Instance;
        Decharge = transform.GetChild(2).GetComponent<ParticleSystem>();
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
                Decharge.Stop();
            }
        }
    }

    public void Activation()
    {
        Temps = 0;
        Actif = true;
        CS.CameraShake();
        GameObject sonelec = Instantiate(Resources.Load<GameObject>("Prefabs/SonElec"), transform.position, transform.rotation);
        Decharge.Play();
    }

    private void OnTriggerStay(Collider other)
    {
        if(Actif && other.GetComponent<IAMovement>() != null && other.GetComponent<IAMovement>().myType == Type.Civilian)
        {
            IAMovement theIA = other.GetComponent<IAMovement>();
            theIA.Hited(transform.position, true, true, true);
        }
    }
}
