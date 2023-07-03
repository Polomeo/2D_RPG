using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public HealthBar healthBarPrefab;
    HealthBar healthBar;

    private void Start()
    {  
        // seteamos los Hitpoints iniciales (heredados de Character)
        hitPoints.value = startingHitPoints;

        // creamos la healthbar
        healthBar = Instantiate(healthBarPrefab);

        // Con esto conectamos el script de HealthBar al Player
        healthBar.character = this;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CanBePickedUp"))
        {
            // Buscamos el ScriptableObject del collision (con minúsucula, porque la referencia al SO está en el atributo "item" de la clase Consumable)
            Item hitObject = collision.gameObject.GetComponent<Consumable>().item;

            if (hitObject != null)
            {
                bool shouldDisappear = false;

                // Imprimimos la propiedad objectName del ScriptableObject
                print("Item: " +  hitObject.objectName);

                // Acciones según el ItemType
                switch (hitObject.itemType)
                {
                    case Item.ItemType.COIN:
                        shouldDisappear = true;
                        break;

                    case Item.ItemType.HEALTH:
                        AdjustHitPoints(hitObject.quantity);
                        shouldDisappear = true;
                        break;
                    
                    default: 
                        break;
                }

                if (shouldDisappear)
                {
                    // Desactivamos el objeto
                    collision.gameObject.SetActive(false);
                }

            }
        }
    }

    public bool AdjustHitPoints(int amount)
    {
        if (hitPoints.value < maxHitPoints)
        {
            // Ajustamos los puntos de vida
            hitPoints.value = hitPoints.value + amount;
            print("Adjusted Hit Points by: " +  amount + ". New value: " + hitPoints.value);

            return true;
        }
        return false;
    }
}
