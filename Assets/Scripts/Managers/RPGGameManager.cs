using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class RPGGameManager : MonoBehaviour
{
    public SpawnPoint playerSpawnPoint;
    
    // ====== MANAGERS ======
    public RPGCameraManager cameraManager;

    // STATIC - Cualquier modificación se aplica a todas las instancias
    public static RPGGameManager Instance = null;

    // SINGLETON - Nos aseguramos que haya UNA sola instancia
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        SetupScene();
    }

    public void SetupScene()
    {
        // PLAYER SPAWN
        SpawnPlayer();
    }

    public void SpawnPlayer()
    {
        if (playerSpawnPoint != null)
        {
            // Spawn el Player
            GameObject player = playerSpawnPoint.SpawnObject();

            // Lo ponemos como objetivo de Follow de la VirtualCamera
            cameraManager.virtualCamera.Follow = player.transform;
        }
    }
}
