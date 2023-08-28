using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    static List<GameObject> ammoPool;
    public GameObject ammoPrefab;
    public int poolSize;
    public float weaponVelocity;

    private void Awake()
    {
        // Creamos la Pool de ammo
        if (ammoPool == null) ammoPool = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject ammoObject = Instantiate(ammoPrefab);
            ammoObject.SetActive(false);
            ammoPool.Add(ammoObject);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            FireAmmo();
        }
    }

    GameObject SpawnAmmo(Vector3 location)
    {
        // Por cada objeto en la Pool
        foreach (GameObject ammo in ammoPool)
        {
            // Si est� desactivado
            if(ammo.activeSelf == false)
            {
                // Activarlo y llevarlo a la posici�n
                ammo.SetActive(true);
                ammo.transform.position = location;

                // Retornarlo y salir del loop
                return ammo;
            }
        }

        // Si no encontr� un objeto desactivado
        return null;
    }

    void FireAmmo()
    {
        // Posici�n del puntero del mouse en la pantalla
        Vector3 mouseCurrentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Traer un Ammo de la pool
        GameObject ammo = SpawnAmmo(transform.position);

        if (ammo != null)
        {
            Arc arcScript = ammo.GetComponent<Arc>();
            float travelDuration = 1.0f / weaponVelocity; // medio segundo
            StartCoroutine(arcScript.TravelArc(mouseCurrentPosition, travelDuration));
        }
    }

    private void OnDestroy()
    {
        ammoPool = null;
    }
}
