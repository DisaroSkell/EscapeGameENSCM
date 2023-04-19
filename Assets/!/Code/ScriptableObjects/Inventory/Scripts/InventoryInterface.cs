using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#nullable enable
public abstract class InventoryInterface : ScriptableObject
{
    public abstract bool AddItem(ItemObject _item);
    public abstract void RemoveItem(ItemObject _item);

    public abstract int GetIndex(ItemObject _item);

    public abstract ItemObject? GetItem(int _slotId);
    public abstract int Count();

    public abstract bool CheckIfItemExistsInInventory(ItemObject itemToCheck);

    public abstract InventorySlot GetSlot(int _slotId);

    public abstract void Clear();

    public void SwitchSlot(InventorySlot slot1, InventorySlot slot2) {
        var temp = slot1.item;
        slot1.item = slot2.item;
        slot2.item = temp;
    }
}

[System.Serializable]
public class InventorySlot
{
    public UserInterface? parent;
    public ItemObject? item;
    public InventorySlot(ItemObject? _item) {
        item = _item;
    }
}