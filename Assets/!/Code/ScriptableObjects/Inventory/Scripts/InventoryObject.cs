using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName= "New Inventory", menuName = "Inventory System/Inventory")]
#nullable enable
public class InventoryObject : InventoryInterface
{
    public List<InventorySlot> Container = new List<InventorySlot>();
    public override bool AddItem(ItemObject _item) {
        Container.Add(new InventorySlot(_item));
        return true;
    }

    public override bool CheckIfItemExistsInInventory(ItemObject itemToCheck)
    {
        return this.Container.Exists(slot => slot.item == itemToCheck);
    }

    public override int Count()
    {
        return this.Container.Count;
    }

    public InventorySlot? GetInventorySlotOfItem(ItemObject itemToCheck)
    {
        return this.Container.Find(slot => slot.item == itemToCheck);
    }

    public override int GetIndex(ItemObject _item)
    {
        return this.Container.FindIndex(slot => slot.item == _item);
    }

    public override InventorySlot GetSlot(int _slotId)
    {
        return this.Container[_slotId];
    }

    public override ItemObject? GetItem(int _slotId)
    {
        if(_slotId >= this.Container.Count) {
            return null;
        }
        return this.Container[_slotId].item;
    }

    public override void RemoveItem(ItemObject _item)
    {
        this.Container.RemoveAll(slot => slot.item == _item);
    }

    public override void Clear()
    {
        this.Container.Clear();
    }
}