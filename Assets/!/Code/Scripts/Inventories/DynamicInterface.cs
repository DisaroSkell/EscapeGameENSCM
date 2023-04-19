using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
#nullable enable
public class DynamicInterface : UserInterface
{
    public int X_START;
    public int Y_START;
    public int X_SPACE_BETWEEN_ITEMS;
    public int NUMBER_OF_CULUMN;
    public int Y_SPACE_BETWEEN_ITEMS;


    public override void UpdateDisplay(bool update=false) {
        for (int i = 0; i < inventory.Count(); i++)
        {
            if(!itemsDisplayed.ContainsKey(inventory.GetSlot(i))) {
                inventory.GetSlot(i).parent = this;
                InstantiateObject(i);
            }
        }
    }

    public override void CreateDisplay()
    {
        for (int i = 0; i < inventory.Count(); i++)
        {
            inventory.GetSlot(i).parent = this;
            InstantiateObject(i);
        }
    }

    protected override void InstantiateObject(int i) {
        ItemObject? item = inventory.GetItem(i);
        GameObject obj;
        
        if(item is null) {
            itemsDisplayed.Add(inventory.GetSlot(i), null);
            return;
        }

        // create a new object with item.prefab and set it at the good position in the inventory
        obj = Instantiate(item.prefab, Vector3.zero, Quaternion.identity, transform);
        obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
        itemsDisplayed.Add(inventory.GetSlot(i), obj);
        slotsOfItems.Add(obj, inventory.GetSlot(i));

        LinkDragEvents(obj);
    }

    /// <summary>
    /// The function returns a Vector3 position based on the index i and predefined constants.
    /// </summary>
    /// <param name="i">The parameter "i" is an integer representing the index of the item for which we
    /// want to calculate the position.</param>
    /// <returns>
    /// A Vector3 object is being returned.
    /// </returns>
    private Vector3 GetPosition(int i) {
        return new Vector3(X_START + (X_SPACE_BETWEEN_ITEMS * (i%NUMBER_OF_CULUMN)), Y_START + (-Y_SPACE_BETWEEN_ITEMS * (i/NUMBER_OF_CULUMN)), 0f);
    }

    protected override void OnEnter(GameObject obj) {
        return;
    }

    protected override void OnDragEnd(GameObject obj) {
        //destroy mouseItem obj attached to it
        Destroy(player.mouseItem.obj);
        player.mouseItem.obj = null;

        // the mouse is hover an slot of an inventory
        if (player.mouseItem.hoverObj) {
            
            // be sure that the item we drag is here
            ItemObject? item = slotsOfItems[obj].item;
            if(item == null) {
                return;
            }
            
            // the hoverSlot has a parent
            if(player.mouseItem.hoverSlot.parent is null) {return;}
            InventorySlot slotHovered = player.mouseItem.hoverSlot.parent.slotsOfItems[player.mouseItem.hoverObj];
            if(slotHovered.item) {
                // put object on a slot not empty
                return;
            }
            // set the item draged to the slot hovered by the mouse
            slotHovered.item = player.mouseItem.itemSlot.item;
            // update the display of the other inventory
            player.mouseItem.hoverSlot.parent.UpdateDisplay(update:true);

            inventory.RemoveItem(item);

            // reset the displayed lists so the updateDisplay method can displayed correctly the items
            itemsDisplayed.Clear();
            slotsOfItems.Clear();
            Transform parent = obj.transform.parent;
            for (int i = 0; i < parent.childCount; i++)
            {
                Destroy(parent.GetChild(i).gameObject);
            }
        }
        else {
            // end the drag on nothing than a slot
        }
        player.mouseItem.itemSlot = null;
        
    }
}
