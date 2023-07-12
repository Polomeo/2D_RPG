using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class RPGGameManager : MonoBehaviour
{
    public SpawnPoint playerSpawnPoint;

    // STATIC - Queda hecha cualquier modificación que le haga cualquier script externo
    public static RPGGameManager instance = null;

    // SINGLETON - Nos aseguramos que haya UNA sola instancia
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
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
            GameObject player = playerSpawnPoint.SpawnObject();
        }
    }
}
