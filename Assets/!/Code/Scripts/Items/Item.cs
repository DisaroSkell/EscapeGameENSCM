using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour, IPointerClickHandler
{
    public ItemObject item;
    public AbstractInventory inventory;

    public event Action<ItemObject> OnItemClicked;

    /// <summary>
    /// This function adds an item to the inventory and destroys the game object when it is clicked.
    /// </summary> 
    public void OnPointerClick(PointerEventData eventData)
    {
        
        inventory.AddItem(this.item);
        if(OnItemClicked is not null) {
            OnItemClicked(item);
        }
        Destroy(this.gameObject);

    } 
}
