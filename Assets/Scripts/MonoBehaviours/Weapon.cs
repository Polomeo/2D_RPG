using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Weapon : MonoBehaviour
{
    static List<GameObject> ammoPool;
    public GameObject ammoPrefab;
    public int poolSize;

    public float weaponVelocity;

    bool isFiring;

    [HideInInspector]
    public Animator animator;

    Camera localCamera;

    float positiveSlope;
    float negativeSlope;

    enum Quadrant
    {
        East,
        South,
        West,
        North
    }


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

    private void Start()
    {
        animator = GetComponent<Animator>();
        isFiring = false;

        localCamera = Camera.main;

        // --- Seteamos los cuadrantes de la Slope para calcular los impactos --- //

        // Puntos de las esquinas de la  pantalla
        // (abajo a la izq = 0,0 --- arriba derecha = total ancho, total alto)
        Vector2 lowerLeft = localCamera.ScreenToWorldPoint(new Vector2(0, 0));
        Vector2 upperRight = localCamera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        Vector2 upperLeft = localCamera.ScreenToWorldPoint(new Vector2(0, Screen.height));
        Vector2 lowerRight = localCamera.ScreenToWorldPoint(new Vector2(Screen.width, 0));

        // Calculamos las líneas oblícuas que cruzan la pantalla
        positiveSlope = GetSlope(lowerLeft, upperRight);
        negativeSlope = GetSlope(upperLeft, lowerRight);
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            isFiring = true;
            FireAmmo();
        }

        UpdateState();
    }

    private void UpdateState()
    {
        throw new NotImplementedException();
    }

    
    // Slope es una línea oblícua
    // Para obtenerla hace (y2 - y1) / (x2 - x1)
    // donde x1, x2 e y1, y2 son puntos donde se choca la oblicua
    // (Teorema de Thales)
    float GetSlope(Vector2 pointOne, Vector2 pointTwo)
    {
        return (pointTwo.y - pointOne.y) / (pointTwo.x  - pointOne.x);
    }

    GameObject SpawnAmmo(Vector3 location)
    {
        // Por cada objeto en la Pool
        foreach (GameObject ammo in ammoPool)
        {
            // Si está desactivado
            if(ammo.activeSelf == false)
            {
                // Activarlo y llevarlo a la posición
                ammo.SetActive(true);
                ammo.transform.position = location;

                // Retornarlo y salir del loop
                return ammo;
            }
        }

        // Si no encontró un objeto desactivado
        return null;
    }

    void FireAmmo()
    {
        // Posición del puntero del mouse en la pantalla
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
