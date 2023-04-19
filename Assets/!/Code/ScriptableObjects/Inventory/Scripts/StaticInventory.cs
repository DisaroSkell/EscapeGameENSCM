using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName= "New Static Inventory", menuName = "Inventory System/Static Inventory")]
public class StaticInventory : InventoryInterface
{

    #nullable enable
    public InventorySlot[] Container = new InventorySlot[10];

    public override bool AddItem(ItemObject _item)
    {
        int i = 0;
        while (i < Container.Length && Container[i] != null)
        {
            i++;
        }
        if(i >= Container.Length) { return false;}
        Container[i] = new InventorySlot(_item);
        return true;
    }

    public override bool CheckIfItemExistsInInventory(ItemObject itemToCheck)
    {
        int i = 0;
        while (i < Container.Length)
        {
            InventorySlot slot = Container[i];
            if(slot.item == itemToCheck) {
                return true;
            }
            i++;
        }
        // all array checked
        return false;
    }

    public override void Clear()
    {
        this.Container = new InventorySlot[this.Container.Length];
    }

    public override int Count()
    {
        int count = 0;
        foreach (var invslot in this.Container)
        {
            if (invslot is not null)
            {
                count++;
            }
        }
        return count;
    }

    public override int GetIndex(ItemObject _item)
    {
        int i = 0;
        while (i < Container.Length)
        {
            InventorySlot slot = Container[i];
            if(slot.item == _item) {
                return i;
            }
            i++;
        }
        // all array checked
        return -1;
    }

    public override ItemObject? GetItem(int _slotId)
    {
        return this.Container[_slotId]?.item;
    }

    public override InventorySlot GetSlot(int _slotId)
    {
        return this.Container[_slotId];
    }

    public override void RemoveItem(ItemObject _item)
    {
        int i = this.GetIndex(_item);
        this.Container[i] = new InventorySlot(null);
    }
}
