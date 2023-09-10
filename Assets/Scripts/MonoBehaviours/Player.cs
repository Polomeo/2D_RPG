using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public HitPoints hitPoints;

    public HealthBar healthBarPrefab;
    HealthBar healthBar;

    public Inventory inventoryPrefab;
    Inventory inventory;

    private void OnEnable()
    {
        ResetCharacter();
    }

    private void Start()
    {  
           
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
                        // Chequeamos si lo podemos añadir al inventario
                        // y si es True, le decimos que desaparezca
                        shouldDisappear = inventory.AddItem(hitObject);
                        break;

                    case Item.ItemType.HEALTH:
                        // Si puede ajustar la vida, devuelve True
                        shouldDisappear = AdjustHitPoints(hitObject.quantity); ;
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

    public override IEnumerator DamageCharacter(int damage, float interval)
    {
        while (true)
        {
            hitPoints.value = hitPoints.value - damage;
            Debug.Log("Player got hit!");
            StartCoroutine(FlickerCharacter());


            if (hitPoints.value <= float.Epsilon)
            {
                KillCharacter();
                break;
            }

            if (interval > float.Epsilon)
            {
                yield return new WaitForSeconds(interval);
            }
            else 
            { 
                break; 
            }
        }
    }
    public override void KillCharacter()
    {
        base.KillCharacter();

        Destroy(healthBar.gameObject);
        Destroy(inventory.gameObject);
    }

    public override void ResetCharacter()
    {
        // Creamos el inventario
        inventory = Instantiate(inventoryPrefab);

        // Creamos la barra de vida y la guardamos en la variable healthBar
        healthBar = Instantiate(healthBarPrefab);

        // Con esto conectamos el script de HealthBar al Player
        healthBar.character = this;

        // Reseteamos los Hitpoints iniciales (heredados de Character)
        hitPoints.value = startingHitPoints;
    }
}
