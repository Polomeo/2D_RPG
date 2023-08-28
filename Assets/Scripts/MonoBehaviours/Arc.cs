using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arc : MonoBehaviour
{
    [SerializeField] private AnimationCurve curve;

    public IEnumerator TravelArc(Vector3 destination, float duration)
    {
        var startPosition = transform.position;
        var percentComplete = 0.0f;

        // Mientras se haya completado menos del 100%
        while(percentComplete < 1.0f)
        {
            // Se le suma el tiempo pasado desde el frame anterior
            // al porcentaje, dividido la duraci�n
            // para que de un n�mero entre 0 y 1 (0% - 100%)
            percentComplete += Time.deltaTime / duration;

            // Se usa la Interpolaci�n lineal (LERP)
            // para ir actualizando la posici�n relativamente al porcentaje
            // y usando curve.Evaluate para el efecto smooth
            transform.position = Vector3.Lerp(startPosition, destination, curve.Evaluate(percentComplete));

            // Espera hasta el pr�ximo frame
            yield return null;
        }

        gameObject.SetActive(false);
    }
}
