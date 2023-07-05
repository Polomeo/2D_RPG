using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    // Creamos un espacio para cargar el Prefab del Slot
    // Creamos una constante que define la cantidad de Slots
    public GameObject slotPrefab;
    public const int numSlots = 5;

    // Inicializamos Arrays que contienen las imágenes y los items,
    // según la cantidad de Slots disponibles.
    Image[] itemImages = new Image[numSlots];
    Item[] items = new Item[numSlots]; // almacena información de tipo Item (Scriptable Object)
    GameObject[] slots = new GameObject[numSlots]; // almacena los slots que iremos creando

    // Método para crear los slots del inventario
    public void CreateSlots()
    {
        if (slotPrefab != null)
        {
            for (int i = 0; i < numSlots; i++)
            {
                // Creamos el Slot y lo nombramos
                GameObject newSlot = Instantiate(slotPrefab);
                newSlot.name = "ItemSlot_" + i.ToString();

                // Lo ponemos como Children del primer Child de InventoryObject (que sería Inventory)
                newSlot.transform.SetParent(gameObject.transform.GetChild(0).transform);

                // Lo agregamos al Array de objetos
                slots[i] = newSlot;

                // Obtenemos su imagen (que está en el child "ItemImage" de Slot)
                // (como al principio va a estar sin asignar, se va a asginar a blanco)
                itemImages[i] = newSlot.transform.GetChild(1).GetComponent<Image>();
            }
        }
    }

    // Método para agregar un ítem al inventario (devuelve T o F)
    public bool AddItem(Item itemToAdd)
    {
        // Iteramos por todos los items del inventario
        for (int i = 0; i < items.Length; i++)
        {
            // Si no es nulo, si coincide el tipo con el que queremos agregar, y si es stackeable
            if (items[i] != null && items[i].itemType == itemToAdd.itemType && itemToAdd.stackable == true)
            {
                // Le sumamos 1 al slot que ya tiene ese item
                items[i].quantity = items[i].quantity + 1;

                // Accedemos al texto del slot y lo activamos
                Slot slotScript = slots[i].gameObject.GetComponent<Slot>();
                TextMeshProUGUI quantityText = slotScript.qtyText;
                quantityText.enabled = true;

                // Establecemos el texto a la cantidad del slot
                quantityText.text = items[i].quantity.ToString();

                return true;
            }

            // Si el slot de item es nulo
            if (items[i] == null)
            {
                // Copiamos el ítem y lo agregamos al inventario
                // (copiamos para no modificar el Scriptable Object original)
                items[i] = Instantiate(itemToAdd);

                // Seteamos su stack en 1
                items[i].quantity = 1;

                // Asignamos la imagen y la activamos para que se muestre
                itemImages[i].sprite = itemToAdd.sprite;
                itemImages[i].enabled = true;

                return true;
            }
        }

        return false;
    }
    
    private void Start()
    {
        CreateSlots();   
    }

    
}
