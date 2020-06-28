using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : Singleton<CameraShaker>
{
    Animator myAnimator;

    private void Awake()
    {
        if (Instance != this)
            Destroy(gameObject);

        myAnimator = GetComponent<Animator>();
    }
    
    public void CameraShake()
    {
        myAnimator.SetTrigger("Shake");
    }
}
