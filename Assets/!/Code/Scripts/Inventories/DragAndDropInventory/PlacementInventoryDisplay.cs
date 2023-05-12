using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlacementInventoryDisplay : AbstractDragAndDropInventoryDisplay
{

    # nullable enable
    public override void Display() {        
        for (int i = 0; i < this.inventory.Length; i++)
        {
            UpdateItemDisplay(i);
        }
    }

    public override void CreateDisplay()
    {
        for (int i = 0; i < this.inventory.Length; i++)
        {
            Transform child = panel.transform.GetChild(i);

            this.objectList.Add(child.gameObject);
            LinkDragEvents(child.gameObject);
        }
        UpdateDisplay();
    }

    private void UpdateItemDisplay(int i) {
        /*
            getchild(i)
            verif si null au cas où => ne rien faire si c'est le cas
            si item present en i dans l'inv : changer l'image
            sinon => mettre l'image de l'empty slot
            */

            if(i >= panel.transform.childCount) return;

            Transform child = panel.transform.GetChild(i);

            ItemObject? item = this.inventory.GetItem(i);

            var rawImg = child.gameObject.GetComponent<RawImage>();
            if(item is not null) {
                rawImg.texture = item.GetPrefab().GetComponent<RawImage>().texture;
                rawImg.color = item.GetPrefab().GetComponent<RawImage>().color;
                if(item.type == ItemType.MatchesCard) {
                    SetObjFlipable(child.gameObject, (DoubleSidedItems)item);
                }
            }
            else {
                rawImg.texture = EmptySlot.gameObject.GetComponent<RawImage>().texture;
                rawImg.color = EmptySlot.gameObject.GetComponent<RawImage>().color;
            }
            
    }

    public override void UpdateDisplay() {
        this.Display();
    }

    protected override void OnDragEndAction(GameObject obj) {
        PlayerMouse mouse = this.player.playerMouse;
        // GUARD: there is an item
        if(mouse.itemFrom is null) return;

        // GUARD: drag end on an inventory
        if(mouse.inventoryDisplayTo is null) return;

        // GUARD: same InventoryDisplay
        if(mouse.inventoryDisplayTo != this) return;

        // GUARD: item has the same type as the inventory
        if(mouse.itemFrom.type != mouse.inventoryDisplayTo.inventory.type) return;
        
        // GUARD: indexes of items are set
        if(mouse.indexFrom < 0 || mouse.indexTo < 0) return;

        this.inventory.Switch(mouse.indexFrom, mouse.indexTo);

    }

    public void SetObjFlipable(GameObject obj, DoubleSidedItems itemObject) {
        // collider to interact with the object
        BoxCollider2D bc = obj.GetComponent<BoxCollider2D>();
        if(bc is null) {
            bc = obj.AddComponent<BoxCollider2D>();
        }
        // attached item to the object
        Item? newItem = obj.GetComponent<Item>();
        if(newItem is null) {
            newItem = obj.AddComponent<Item>();
        }
        newItem.item = itemObject;
        newItem.inventory = itemObject.inventory;
        // add event on click to flip item and update display
        UnityAction handleItemClickedUnityAction = () => { this.HandleItemClicked(itemObject); };
        newItem.OnPointerClickEvent.RemoveAllListeners();
        newItem.OnPointerClickEvent.AddListener(handleItemClickedUnityAction);
    }

    public void HandleItemClicked(DoubleSidedItems item) {
        item.Flip();
        this.UpdateDisplay();
    }

    
}
