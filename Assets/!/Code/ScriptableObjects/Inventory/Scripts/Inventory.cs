using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


[CreateAssetMenu(fileName= "New Inventory", menuName = "Inventory System/Inventory (New)")]
public class Inventory : AbstractInventory
{
    #nullable enable

    public ItemObject?[] Container = new ItemObject?[9];

    public override int Length => this.Container.Length;

    /// <summary>
    /// This function adds an item to the inventory and returns true if successful, false if the inventory is
    /// full.
    /// </summary>
    /// <param name="ItemObject">ItemObject is a class or data type that represents an item that can be
    /// added to an inventory.</param>
    /// <returns>
    /// The method is returning a boolean value. It returns true if the item was successfully added to
    /// the inventory, and false if the inventory is already full and the item cannot be added.
    /// </returns>
    public override bool AddItem(ItemObject _item)
    {
        // inventory has a type, but the _item has not the same type
        if(this.type != ItemType.All && this.type != _item.type) {
            return false;
        }
        int i = this.firstIndexNull();
        if(i < 0) {
            // object not added because array is full
            return false;
        }
        Container[i] = _item;
        return true;
    }

    /// <summary>
    /// The function returns the index of the first null element in an array, or -1 if there are no null
    /// elements.
    /// </summary>
    /// <returns>
    /// The method `firstIndexNull()` returns an integer value. If the loop condition is never met
    /// (i.e., all elements in the `Container` array are not null), the method returns -1. Otherwise, it
    /// returns the index of the first null element in the `Container` array.
    /// </returns>
    private int firstIndexNull() {
        int i = 0;
        while (i < Container.Length && Container[i] != null)
        {
            i++;
        }
        if(i >= Container.Length) { return -1;}
        return i;
    }

    /// <summary>
    /// remove all items from the inventory.
    /// </summary>
    public override void Clear()
    {
        this.Container = new ItemObject?[this.Container.Length];
    }

    
    
    /// <summary>
    /// This function counts the number of non-null items in the inventory.
    /// </summary>
    /// <returns>
    /// The method is returning an integer value which represents the number of non-null items in the
    /// inventory.
    /// </returns>
    public override int Count()
    {
        int count = 0;
        foreach (var invitem in this.Container)
        {
            if (invitem is not null)
            {
                count++;
            }
        }
        return count;
    }

    /// <summary>
    /// This function returns the index of an item in the inventory, or -1 if the item is not found.
    /// </summary>
    /// <param name="ItemObject">ItemObject is a class or data structure that represents an item in an
    /// inventory. 
    /// </param>
    /// <returns>
    /// If the item is found in the inventory, the index of the item is returned. If the item is not
    /// found in the inventory, -1 is returned.
    /// </returns>
    public override int GetIndex(ItemObject _item)
    {
        int i = 0;
        while (i < Container.Length)
        {
            if(Container[i] == _item) {
                return i;
            }
            i++;
        }
        // all array checked
        return -1;
    }

    /// <summary>
    /// This function returns an item object from the inventory at a specified slot ID, or null if the
    /// slot ID is out of range.
    /// </summary>
    /// <param name="_slotId">_slotId is an integer parameter representing the index of the item slot in
    /// the inventory. It is used to retrieve the item object stored in that particular
    /// slot.</param>
    /// <returns>
    /// The method is returning an object of type `ItemObject` or `null`.
    /// </returns>
    public override ItemObject? GetItem(int _slotId)
    {
        if(_slotId >= this.Container.Length) {return null;}
        if(_slotId < 0) {return null;}
        return this.Container[_slotId];
    }

    /// <summary>
    /// This function removes an item from the inventory.
    /// </summary>
    /// <param name="ItemObject">
    /// ItemObject is a class or data structure that represents an item in an inventory.
    /// </param>
    public override void RemoveItem(ItemObject _item)
    {
        this.RemoveItem(this.GetIndex(_item));
    }

    /// <summary>
    /// This function removes an item from the inventory at a specified slot ID.
    /// </summary>
    /// <param name="_slotId">_slotId is an integer parameter that represents the index of the item slot
    /// in the inventory that needs to be removed.</param>
    /// <returns>
    /// If the `_slotId` is out of range, then the method returns without doing anything. Otherwise, 
    /// it removes the item at the specified slot ID.
    /// </returns>
    public override void RemoveItem(int _slotId) {
        if(_slotId >= this.Container.Length) { return; }
        if(_slotId < 0) { return; }

        this.Container[_slotId] = null;
    }

    /// <summary>
    /// This function switches the positions of two items in the inventory.
    /// </summary>
    /// <param name="_slotId1">An integer representing the index of the first item slot in the inventory.</param>
    /// <param name="_slotId2">An integer representing the index of the second item slot in the inventory.</param>
    /// <returns>
    /// If either `_slotId1` or `_slotId2` is out of range, the method returns without doing anything. Otherwise, the method switches the
    /// items at the specified slots in the inventory.
    /// </returns>
    public override void Switch(int _slotId1, int _slotId2)
    {
        if(_slotId1 >= this.Container.Length || _slotId2 >= this.Container.Length) { return; }
        if(_slotId1 < 0 || _slotId2 < 0) { return; }

        ItemObject? temp = this.Container[_slotId1];
        this.Container[_slotId1] = this.Container[_slotId2];
        this.Container[_slotId2] = temp;

    }

    public override bool SetItem(int _index, ItemObject? _item)
    {
        if(_item is null) return false;
        // inventory has a type, but the _item has not the same type
        if(this.type != ItemType.All && this.type != _item.type) return false;
        if(_index >= this.Container.Length) return false;
        if(_index < 0) return false;
        if(this.GetItem(_index) is not null) return false;

        this.Container[_index] = _item;
        return true;
    }

    public static bool Equals(Inventory inv1, Inventory inv2) {
        if(inv1.Length != inv2.Length) return false;
        Debug.Log("La longueur est la même");
        int i = 0;
        while(i < inv1.Length) {
            ItemObject? item = inv1.GetItem(i);
            if(item is not null) {
                if(!item.Equals(inv2.GetItem(i))) {
                    return false;
                }
            }
            i++;
        }
        return true;
    }
}
