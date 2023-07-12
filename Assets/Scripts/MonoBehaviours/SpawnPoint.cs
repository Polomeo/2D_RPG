using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject prefabToSpawn;
    public float repeatInterval;

    public void Start()
    {
        if (repeatInterval > 0)
        {
            InvokeRepeating("SpawnObject", 0.0f, repeatInterval);
        }
    }

    // Si hay un Prefab para Spawn, devuelve la instancia como GameObject
    public GameObject SpawnObject()
    {
        if (prefabToSpawn != null) 
        {
            return Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
        }
    
        return null;
    }
}
