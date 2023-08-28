using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public int damageInflicted;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Detectamos que esté chocando con el collider del enemigo
        // y no con el CircleCollider2D que detecta la cercanía con el player
        if (collision is BoxCollider2D)
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            StartCoroutine(enemy.DamageCharacter(damageInflicted, 0));
            gameObject.SetActive(false);
        }
    }
}
