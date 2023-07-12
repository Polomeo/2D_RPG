using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGCameraManager : MonoBehaviour
{
    [HideInInspector]
    public CinemachineVirtualCamera virtualCamera;

    public static RPGCameraManager Instance = null;

    // SINGLETON
    private void Awake()
    {
        // SINGLETON - Lógica
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        // VIRTUAL CAMERA - Encontrarla en la escena
        GameObject vCamGameObject = GameObject.FindWithTag("VirtualCamera");
        virtualCamera = vCamGameObject.GetComponent<CinemachineVirtualCamera>();
    }
}
