using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;

#nullable enable
public class DynamicInventoryDisplay : AbstractDragAndDropInventoryDisplay {

    public int X_START;
    public int Y_START;
    public int X_SPACE_BETWEEN_ITEMS;
    public int NUMBER_OF_CULUMN;
    public int Y_SPACE_BETWEEN_ITEMS;

    public bool takeInventory;
    public override void Display() {
        for (int i = 0; i < inventory.Length; i++) {
            InstantiateObject(i);
        }
    }

    private void InstantiateObject(int i) {
        ItemObject? item = inventory.GetItem(i);
        GameObject prefab;
        
        if(item is null) {
            prefab = EmptySlot;
        }
        else {
            prefab = item.prefab;
        }

        // create a new object with item.prefab and set it at the good position in the inventory
        GameObject obj = Instantiate(prefab, Vector3.zero, Quaternion.identity, transform);
        obj.transform.SetParent(panel.transform);
        obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
        this.objectList.Add(obj);

        if(this.takeInventory) {
            if(item is not null) {
                SetObjClickable(obj, item);
            }
        }
        else {
            LinkDragEvents(obj);
        }
    }

    private void SetObjClickable(GameObject obj, ItemObject itemObject) {
        BoxCollider2D bc = obj.AddComponent<BoxCollider2D>();
        Item newItem = obj.AddComponent<Item>();
        newItem.item = itemObject;
        newItem.inventory = itemObject.inventory;

        newItem.OnItemClicked += HandleItemClicked;
    }

    private void HandleItemClicked(ItemObject item) {
        this.inventory.RemoveItem(item);
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

    protected override void OnDragEndAction(GameObject obj)
    {
        PlayerMouse mouse = this.player.playerMouse;
        // GUARD: there is an item
        if(mouse.itemFrom is null) return;

        // GUARD: drag end on an inventory
        if(mouse.inventoryDisplayTo is null) return;
        
        // GUARD: indexes of items are set
        if(mouse.indexFrom < 0 || mouse.indexTo < 0) return;

        if(mouse.inventoryDisplayTo.GetType() == typeof(PlacementInventoryDisplay)) {
            EndDragOnPlacementInventory();
        }
        else if (mouse.inventoryDisplayTo == this) {
            EndDragOnSameInventory();
        }
    }

    private void EndDragOnSameInventory() {
        PlayerMouse mouse = this.player.playerMouse;

        // interaction code
        Debug.Log(
            string.Format("interaction between inventory[{0}] and inventory[{1}]", mouse.indexFrom, mouse.indexTo)
        );
    }

    
    private void EndDragOnPlacementInventory() {
        PlayerMouse mouse = this.player.playerMouse;
        // GUARD: item has the same type as the inventory
        AbstractDragAndDropInventoryDisplay? inv = mouse.inventoryDisplayTo;
        if(inv is null) return;
        ItemType? invType = inv.inventory.type;
        if(invType != ItemType.All && mouse.itemFrom?.type != invType) return;
        inv.inventory.SetItem(mouse.indexTo, mouse.itemFrom);

        // update other inventory display
        inv.UpdateDisplay();
        if(mouse.itemFrom is not null) {
            this.inventory.RemoveItem(mouse.itemFrom);
        }

    }

    public override void CreateDisplay()
    {
        this.UpdateDisplay();
    }
}
