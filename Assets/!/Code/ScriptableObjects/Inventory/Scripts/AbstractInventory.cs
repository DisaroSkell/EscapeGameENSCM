using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class AbstractInventory : ScriptableObject
{
    public ItemType type;

    #nullable enable
    public abstract int Length { get; }
    public abstract bool AddItem(ItemObject _item);
    public abstract void RemoveItem(ItemObject _item);
    public abstract void RemoveItem(int _slotId);

    public abstract int GetIndex(ItemObject _item);

    public abstract ItemObject? GetItem(int _slotId);

    public abstract int Count();

    public abstract void Clear();

    public abstract void Switch(int _slotId1, int _slotId2);

    public abstract bool SetItem(int _index, ItemObject? _item);
}
