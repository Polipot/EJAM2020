﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : Singleton<CameraShake>
{
    Vector3 cameraInitialPosition;
    public float shakeMagnetude = 0.04f, shakeTime = 0.15f;

    private void Awake()
    {
        if (Instance != this)
            Destroy(gameObject);
    }

    public void ShakeIt()
    {
        cameraInitialPosition = Camera.main.transform.localPosition;
        InvokeRepeating("StartCameraShaking", 0f, 0.005f);
        Invoke("StopCameraShaking", shakeTime);
    }

    public void StartCameraShaking()
    {
        float cameraShakingOffsetX = Random.value * shakeMagnetude * 2 - shakeMagnetude;
        float cameraShakingOffsetY = Random.value * shakeMagnetude * 2 - shakeMagnetude;
        Vector3 cameraIntermadiatePosition = Camera.main.transform.localPosition;
        cameraIntermadiatePosition.x += cameraShakingOffsetX;
        cameraIntermadiatePosition.y += cameraShakingOffsetY;
        Camera.main.transform.localPosition = cameraIntermadiatePosition;
    }

    void StopCameraShaking()
    {
        CancelInvoke("StartCameraShaking");
        Camera.main.transform.localPosition = new Vector3(0,10,0);
    }
}

