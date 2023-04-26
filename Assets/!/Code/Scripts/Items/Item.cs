using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour, IPointerClickHandler
{
    public ItemObject item;
    public AbstractInventory inventory;

    // public event Action<ItemObject> OnItemClicked;

    [SerializeField] public UnityEvent OnPointerClickEvent = new UnityEvent();

    /// <summary>
    /// This function adds an item to the inventory and destroys the game object when it is clicked.
    /// </summary> 
    public void OnPointerClick(PointerEventData eventData) {
        OnPointerClickEvent.Invoke();
    } 
    public void PickUpItem() {
        inventory.AddItem(this.item);
        Destroy(this.gameObject);
    }

    
}
