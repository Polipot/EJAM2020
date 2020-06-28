using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum aPiege { Barbecue }

public class Piege : MonoBehaviour
{
    CameraShaker CS;
    public IAMovement theUser;
    public bool Amorçé;

    public aPiege PiegeType;

    // Start is called before the first frame update
    void Awake()
    {
        CS = CameraShaker.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Utilisation(IAMovement theIA)
    {
        Amorçé = false;
        CS.CameraShake();
        theIA.Hited(transform.position);

        switch (PiegeType)
        {
            case aPiege.Barbecue:
                GameObject theFeu = Instantiate(Resources.Load<GameObject>("Prefabs/Feu"), theIA.transform.position, Resources.Load<GameObject>("Prefabs/Feu").transform.rotation);
                theFeu.GetComponent<enFeu>().cible = theIA.transform;
                break;
        }
    }
}
