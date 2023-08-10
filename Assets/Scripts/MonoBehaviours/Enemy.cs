using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public int damageStrength;
    Coroutine damageCoroutine;

    float hitPoints;

    // Como hereda de Character, debe implementar todos sus métodos abstractos
    public override IEnumerator DamageCharacter(int damage, float interval)
    {
        while (true)
        {
            hitPoints = hitPoints - damage;

            // float.Epison --> float más pequeño > 0
            if(hitPoints <= float.Epsilon)
            {
                KillCharacter();
                break; // Sale del bucle While
            }

            // Si el intervalo es mayor a 0
            if (interval > float.Epsilon)
            {
                // Espera el tiempo del intervalo para volver a atacar
                yield return new WaitForSeconds(interval);
            }
            else
            {
                // Sino salir del bucle
                break;
            }
        }
    }
    public override void ResetCharacter()
    {
        hitPoints = startingHitPoints; // declarada en Character
    }

    // Se llama cuando el objeto está encendido y el componente (script) está activo
    private void OnEnable()
    {
        ResetCharacter();
        damageCoroutine = null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (damageCoroutine  == null) 
            {
                // Asignamos la coroutine de daño para que se asigne una sóla vez
                // Ejecutamos la rutina de daño en el player
                damageCoroutine = StartCoroutine(player.DamageCharacter(damageStrength, 1.0f));
                Debug.Log("Damaging player!");
            }

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Exit collision with player!");
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }
        }
    }
}
