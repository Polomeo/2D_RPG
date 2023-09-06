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
            // al porcentaje, dividido la duración
            // para que de un número entre 0 y 1 (0% - 100%)
            percentComplete += Time.deltaTime / duration;

            // La función Seno completa un ciclo en 2pi
            // A porcentaje = 0 empieza en el eje de coord.
            // A porcentaje = 0.5 es 1/2pi, por lo que está en el tope de la cresta
            // A porcentaje = 1 es 1pi, por lo que vuelve a X = 0
            var currentHeight = Mathf.Sin(Mathf.PI * percentComplete);

            // Se usa la Interpolación lineal (LERP)
            // para ir actualizando la posición relativamente al porcentaje
            // y usando curve.Evaluate para el efecto smooth
            // (evalúa la curva en la distancia recorrida en el porcentaje declarado)
            // Le sumamos un Vector3.up * la altura basada en la curva Seno
            transform.position = Vector3.Lerp(startPosition, destination, curve.Evaluate(percentComplete)) + Vector3.up * currentHeight;


            // Espera hasta el próximo frame
            yield return null;
        }

        gameObject.SetActive(false);
    }
}
