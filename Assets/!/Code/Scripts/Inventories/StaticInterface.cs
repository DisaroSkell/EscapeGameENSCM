using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaticInterface : UserInterface
{
    public GameObject[] slots;

    public GameObject EmptySlot;

    #nullable enable
    protected override void InstantiateObject(int i)
    {
        ItemObject? item = inventory.GetItem(i);
        if(!itemsDisplayed.ContainsKey(inventory.GetSlot(i))) {
            return;
        }
        GameObject? slotObj = itemsDisplayed[inventory.GetSlot(i)];
        if(slotObj is null) {
            return;
        }

        var rawImg = slotObj.gameObject.GetComponent<RawImage>();
        if(item is not null) {
            rawImg.texture = item.prefab.GetComponent<RawImage>().texture;
            rawImg.color = new Color(1.0f, 1.0f, 1.0f,1.0f);
        }
        else {
            rawImg.texture = EmptySlot.gameObject.GetComponent<RawImage>().texture;
            rawImg.color = EmptySlot.gameObject.GetComponent<RawImage>().color;
        }
        
    }

    public override void UpdateDisplay(bool update)
    {
        if(update) {
            for (int i = 0; i < inventory.Count(); i++)
            {
                InstantiateObject(i);
            }
        }
    }

    public override void CreateDisplay() {
        for (int i = 0; i < inventory.Count(); i++)
        {
            inventory.GetSlot(i).parent = this;
            InstantiateObject(i);
            GameObject obj = this.slots[i];
            itemsDisplayed.Add(inventory.GetSlot(i), obj);
            slotsOfItems.Add(obj, inventory.GetSlot(i));
            LinkDragEvents(obj);
        }
    }

    protected override void OnEnter(GameObject obj) {
        player.mouseItem.hoverObj = obj;
        if(inventory.GetType() == typeof(StaticInventory)) {
            if (slotsOfItems.ContainsKey(obj)){
                player.mouseItem.hoverSlot = slotsOfItems[obj];
            }
        }

    }

    protected override void OnDragEnd(GameObject obj) {
        Destroy(player.mouseItem.obj);
        player.mouseItem.obj = null;
        
        if (player.mouseItem.hoverObj) {
            
            // mouse is hover an obj where item can be placed in
            ItemObject? item = slotsOfItems[obj].item;
            if(item == null) {
                return;
            }
            
            if(player.mouseItem.hoverSlot.parent is null) {return;}
            InventorySlot slotHovered = player.mouseItem.hoverSlot.parent.slotsOfItems[player.mouseItem.hoverObj];

            // IF ITEM MOVED TO THE SAME INVENTORY
            if(slotHovered.parent == player.mouseItem.itemSlot.parent) {
                inventory.SwitchSlot(slotHovered, player.mouseItem.itemSlot);
                UpdateDisplay(update:true);
            }
            // the slot is not empty in the other inventory
            if(slotHovered.item) { return;}
            slotHovered.item = player.mouseItem.itemSlot.item;

            inventory.RemoveItem(item);
        }
        player.mouseItem.itemSlot = null;
    }
}
